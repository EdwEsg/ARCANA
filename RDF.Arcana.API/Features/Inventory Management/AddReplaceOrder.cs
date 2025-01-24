
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-replace-order"), ApiController]
    public class AddReplaceOrder : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddReplaceOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddReplaceOrderCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AccessBy = userId;
                }
                var result = await _mediator.Send(command);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class AddReplaceOrderCommand : IRequest<Result>
        {
            public int ReturnOrderId { get; set; }
            public int AccessBy { get; set; }

            public ICollection<ReplaceOrdersDto> ReplaceOrders { get; set; } = new List<ReplaceOrdersDto>();

            public class ReplaceOrdersDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public string Bbd { get; set; }
                public string Reason { get; set; }
            }
        }

        public class Handler : IRequestHandler<AddReplaceOrderCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddReplaceOrderCommand request, CancellationToken cancellationToken)
            {
                bool isUserCdo = await _context.Users
                    .Where(u => u.Id == request.AccessBy)
                    .Select(u => u.UserRolesId)
                    .FirstOrDefaultAsync(cancellationToken) == 6;
                if (!isUserCdo)
                {
                    return InventoryErrors.NotUserCdo();
                }

                var returnedOrder = await _context.ReturnedOrders
                    .Include(ro => ro.ReturnOrderItems)
                    .FirstOrDefaultAsync(ro => ro.Id == request.ReturnOrderId, cancellationToken);

                if (returnedOrder == null)
                    return InventoryErrors.NoReturnFound();

                if (returnedOrder.Status != Status.Pending)
                    return InventoryErrors.InvalidStatus();

                var replaceOrders = request.ReplaceOrders.ToList();

                if (replaceOrders.Any())
                {
                    var groupedReplace = replaceOrders
                        .GroupBy(r => r.ItemId)
                        .Select(g => new
                        {
                            ItemId = g.Key,
                            RequestedQuantity = g.Sum(x => x.Quantity),
                            Price = g.First().Price 
                        })
                        .ToList();

                    var cdoMoveOrderItems = await _context.MoveOrderItems
                        .Where(m => m.CreatedBy.Id == request.AccessBy)
                        .GroupBy(i => i.ItemId)
                        .Select(g => new
                        {
                            ItemId = g.Key,
                            AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
                        })
                        .ToListAsync(cancellationToken);

                    var cdoTransferOrderItems = await _context.TransferOrderItems
                        .Where(t => t.TransferOrder.TransferToId == request.AccessBy &&
                                    t.TransferOrder.Status == Status.Received)
                        .GroupBy(i => i.ItemId)
                        .Select(g => new
                        {
                            ItemId = g.Key,
                            AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
                        })
                        .ToListAsync(cancellationToken);

                    var totalOrderItems = cdoMoveOrderItems
                        .Concat(cdoTransferOrderItems)
                        .GroupBy(x => x.ItemId)
                        .Select(g => new
                        {
                            ItemId = g.Key,
                            AvailableQuantity = g.Sum(x => x.AvailableQuantity)
                        })
                        .ToList();

                    foreach (var item in groupedReplace)
                    {
                        var itemCode = await _context.Items
                            .Where(i => i.Id == item.ItemId)
                            .Select(i => i.ItemCode)
                            .FirstOrDefaultAsync(cancellationToken);

                        var availableItems = totalOrderItems.FirstOrDefault(a => a.ItemId == item.ItemId);

                        if (availableItems == null)
                            return InventoryErrors.ItemNotFound(itemCode);

                        if (item.RequestedQuantity > availableItems.AvailableQuantity)
                        {
                            return InventoryErrors.InsufficientQuantity(
                                itemCode,
                                item.RequestedQuantity,
                                availableItems.AvailableQuantity
                            );
                        }
                    }

                    foreach (var item in groupedReplace)
                    {
                        decimal quantityToDeduct = item.RequestedQuantity;

                        var transferOrderItems = await _context.TransferOrderItems
                            .Where(t => t.TransferOrder.TransferToId == request.AccessBy &&
                                        t.TransferOrder.Status == Status.Received &&
                                        t.ItemId == item.ItemId &&
                                        t.IsActive &&
                                        t.RemainingQuantity > 0)
                            .OrderBy(t => t.TransferOrder.TransactionDate)
                            .ToListAsync(cancellationToken);

                        foreach (var toItem in transferOrderItems)
                        {
                            if (quantityToDeduct <= 0) break;

                            var available = toItem.RemainingQuantity ?? 0;
                            if (available >= quantityToDeduct)
                            {
                                toItem.RemainingQuantity -= quantityToDeduct;
                                quantityToDeduct = 0;
                            }
                            else
                            {
                                toItem.RemainingQuantity = 0;
                                quantityToDeduct -= available;
                            }
                        }

                        if (quantityToDeduct > 0)
                        {
                            var moveOrderItems = await _context.MoveOrderItems
                                .Where(m =>
                                    m.CreatedBy.Id == request.AccessBy &&
                                    m.ItemId == item.ItemId &&
                                    m.IsActive &&
                                    m.RemainingQuantity > 0)
                                .OrderBy(m => m.MoveOrder.CreatedDate)
                                .ToListAsync(cancellationToken);

                            foreach (var moItem in moveOrderItems)
                            {
                                if (quantityToDeduct <= 0) break;

                                var available = moItem.RemainingQuantity ?? 0;
                                if (available >= quantityToDeduct)
                                {
                                    moItem.RemainingQuantity -= quantityToDeduct;
                                    quantityToDeduct = 0;
                                }
                                else
                                {
                                    moItem.RemainingQuantity = 0;
                                    quantityToDeduct -= available;
                                }
                            }
                        }

                        if (quantityToDeduct > 0)
                        {
                            throw new InvalidOperationException(
                                $"Unable to fully deduct quantity for ItemId '{item.ItemId}'. Remaining to deduct: {quantityToDeduct}"
                            );
                        }
                    }

                    decimal totalExchange = 0;
                    var replaceOrderItems = new List<ReplaceOrderItem>();
                    foreach (var r in request.ReplaceOrders)
                    {
                        replaceOrderItems.Add(new ReplaceOrderItem
                        {
                            ReturnOrderId = returnedOrder.Id,
                            ItemId = r.ItemId,
                            Quantity = r.Quantity,
                            Price = r.Price,
                            Bbd = r.Bbd,
                            Reason = r.Reason
                        });
                        totalExchange += (r.Price * r.Quantity);
                    }
                    _context.ReplaceOrderItems.AddRange(replaceOrderItems);

                    returnedOrder.TotalExchange = totalExchange;
                }

                returnedOrder.Status = "Received";


                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}