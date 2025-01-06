using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-freebie-order"), ApiController]
    public class AddFreebieOrder : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddFreebieOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddFreebieOrderCommand command)
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

        public class AddFreebieOrderCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public int AccessBy { get; set; }
            public ICollection<FreebieOrderItemDto> FreebieOrderItems { get; set; }
            public class FreebieOrderItemDto
            {
                public int ItemId { get; set; }
                public decimal? Quantity { get; set; }
            }
        }


        public class Handler : IRequestHandler<AddFreebieOrderCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddFreebieOrderCommand request, CancellationToken cancellationToken)
            {
                var isUserCdo = await _context.Users
                    .Where(u => u.Id == request.AccessBy)
                    .Select(u => u.UserRolesId)
                    .FirstOrDefaultAsync(cancellationToken) == 6;

                if (!isUserCdo)
                {
                    return InventoryErrors.NotUserCdo();
                }

                bool existingClient = _context.Clients
                    .Any(u => u.Id == request.ClientId);

                if (!existingClient)
                {
                    return InventoryErrors.NoClientFound();
                }

                var requestedItems = request.FreebieOrderItems
                    .GroupBy(i => i.ItemId)
                    .Select(g => new
                    {
                        ItemId = g.Key,
                        RequestedQuantity = g.Sum(i => i.Quantity ?? 0)
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
                    .Where(t => t.TransferOrder.TransferToId == request.AccessBy)
                    .GroupBy(i => i.ItemId)
                    .Select(g => new
                    {
                        ItemId = g.Key,
                        AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
                    }).ToListAsync(cancellationToken);

                var totalAvailableItems = cdoMoveOrderItems
                    .Concat(cdoTransferOrderItems)
                    .GroupBy(x => x.ItemId)
                    .Select(g => new
                    {
                        ItemId = g.Key,
                        AvailableQuantity = g.Sum(x => x.AvailableQuantity)
                    })
                    .ToList();

                foreach (var reqItem in requestedItems)
                {
                    var itemCode = await _context.Items
                        .Where(i => i.Id == reqItem.ItemId)
                        .Select(i => i.ItemCode)
                        .FirstOrDefaultAsync(cancellationToken);

                    var availableItem = totalAvailableItems.FirstOrDefault(a => a.ItemId == reqItem.ItemId);

                    if (availableItem == null)
                    {
                        return InventoryErrors.ItemNotFound(itemCode);
                    }

                    if (reqItem.RequestedQuantity > availableItem.AvailableQuantity)
                    {
                        return InventoryErrors.InsufficientQuantity(
                            itemCode,
                            reqItem.RequestedQuantity,
                            availableItem.AvailableQuantity
                        );
                    }
                }

                foreach (var reqItem in requestedItems)
                {
                    decimal quantityToDeduct = reqItem.RequestedQuantity;

                    var transferOrderItems = await _context.TransferOrderItems
                        .Where(t =>
                            t.TransferOrder.TransferToId == request.AccessBy &&
                            t.ItemId == reqItem.ItemId &&
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
                                m.ItemId == reqItem.ItemId &&
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
                            $"Unable to fully deduct quantity for ItemId '{reqItem.ItemId}'. Remaining: {quantityToDeduct}"
                        );
                    }
                }

                var freebieOrder = new FreebieOrder
                {
                    ClientId = request.ClientId,
                    TransactionType = Status.Freebie,
                    TotalQuantity = requestedItems.Sum(i => i.RequestedQuantity),
                    CreatedById = request.AccessBy,
                    CreatedDate = DateTime.Now
                };

                _context.FreebieOrders.Add(freebieOrder);
                await _context.SaveChangesAsync(cancellationToken);

                var freebieOrderItems = requestedItems.Select(i => new FreebieOrderItems
                {
                    FreebieOrderId = freebieOrder.Id,
                    ItemId = i.ItemId,
                    Quantity = i.RequestedQuantity,
                    Bbd = "", 
                    IsActive = true
                }).ToList();

                _context.FreebieOrderItems.AddRange(freebieOrderItems);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}
