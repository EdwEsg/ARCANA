using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-return-order"), ApiController]
    public class AddReturnOrder : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddReturnOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddReturnOrderCommand command)
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

        public class AddReturnOrderCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public int AccessBy { get; set; }
            public ICollection<ReturnOrdersDto> ReturnOrders { get; set; }
            public ICollection<ReplaceOrdersDto> ReplaceOrders { get; set; }
            public class ReturnOrdersDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public string Bbd { get; set; }
            }

            public class ReplaceOrdersDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public string Bbd { get; set; }
            }
        }

        public class Handler : IRequestHandler<AddReturnOrderCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddReturnOrderCommand request, CancellationToken cancellationToken)
            {
                bool isUserCdo = await _context.Users
                    .Where(u => u.Id == request.AccessBy)
                    .Select(u => u.UserRolesId)
                    .FirstOrDefaultAsync(cancellationToken) == 6; //Cdo

                if (!isUserCdo)
                {
                    return InventoryErrors.NotUserCdo();
                }

                bool isClientExist = _context.Clients
                    .Any(c => c.Id == request.ClientId);

                if (!isClientExist)
                {
                    return InventoryErrors.NoClientFound();
                }

                var replaceOrders = request.ReplaceOrders
                    .GroupBy(o => o.ItemId)
                    .Select(g => new
                    {
                        ItemId = g.Key,
                        RequestedQuantity = g.Sum(o => o.Quantity),
                    }).ToList();

                var cdoMoveOrderItems = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == request.AccessBy)
                    .GroupBy(i => i.ItemId)
                    .Select(g => new
                    {
                        ItemId = g.Key,
                        AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
                    }).ToListAsync(cancellationToken);

                var cdoTransferOrderItems = await _context.TransferOrderItems
                    .Where(t => t.TransferOrder.TransferToId == request.AccessBy &&
                        t.TransferOrder.Status == Status.Received)
                    .GroupBy(i => i.ItemId)
                    .Select(g => new
                    {
                        ItemId = g.Key,
                        AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
                    }).ToListAsync(cancellationToken);

                var totalOrderItems = cdoMoveOrderItems
                    .Concat(cdoTransferOrderItems)
                    .GroupBy(x => x.ItemId)
                    .Select(g => new
                    {
                        ItemId = g.Key,
                        AvailableQuantity = g.Sum(x => x.AvailableQuantity)
                    }).ToList();

                foreach (var item in replaceOrders)
                {
                    var itemCode = await _context.Items
                        .Where(i => i.Id == item.ItemId)
                        .Select(i => i.ItemCode)
                        .FirstOrDefaultAsync(cancellationToken);

                    var availableItems = totalOrderItems.FirstOrDefault(a => a.ItemId == item.ItemId);

                    if (availableItems == null)
                    {
                        return InventoryErrors.ItemNotFound(itemCode);
                    }

                    if (item.RequestedQuantity > availableItems.AvailableQuantity)
                    {
                        return InventoryErrors.InsufficientQuantity(
                            itemCode,
                            item.RequestedQuantity,
                            availableItems.AvailableQuantity
                        );
                    }
                }

                foreach (var item in replaceOrders)
                {
                    decimal quantityToDeduct = item.RequestedQuantity;

                    var transferOrderItems = await _context.TransferOrderItems
                        .Where(t =>
                            t.TransferOrder.TransferToId == request.AccessBy &&
                            t.TransferOrder.Status == Status.Received &&
                            t.ItemId == item.ItemId &&
                            t.IsActive &&
                            t.RemainingQuantity > 0
                        )
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
                                m.RemainingQuantity > 0
                            )
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

                decimal totalReturn = request.ReturnOrders.Sum(x => x.Price * x.Quantity);
                decimal totalExchange = request.ReplaceOrders.Sum(x => x.Price * x.Quantity);

                var returnedOrder = new ReturnedOrder
                {
                    ClientId = request.ClientId,
                    TotalReturn = totalReturn,
                    TotalExchange = totalExchange,
                    CreatedbyId = request.AccessBy,
                    CreatedDate = DateTime.Now
                };

                _context.ReturnedOrders.Add(returnedOrder);
                await _context.SaveChangesAsync(cancellationToken);

                var returnOrderItems = request.ReturnOrders.Select(r => new ReturnOrderItem
                {
                    ReturnOrderId = returnedOrder.Id,
                    ItemId = r.ItemId,
                    Quantity = r.Quantity,
                    Price = r.Price,
                    Bbd = r.Bbd,
                    RemainingQuantity = r.Quantity
                }).ToList();

                _context.ReturnOrderItems.AddRange(returnOrderItems);

                var replaceOrderItems = request.ReplaceOrders.Select(r => new ReplaceOrderItem
                {
                    ReturnOrderId = returnedOrder.Id,
                    ItemId = r.ItemId,
                    Quantity = r.Quantity,
                    Price = r.Price,
                    Bbd = r.Bbd
                }).ToList();

                _context.ReplaceOrderItems.AddRange(replaceOrderItems);

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}
