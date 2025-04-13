
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    //THIS IS TRANSFER OUT
    [Route("api/add-transfer-out"), ApiController]
    public class AddTransferOut : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddTransferOut(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTransferInCommand command)
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

        public class AddTransferInCommand : IRequest<Result>
        {
            public int To { get; set; }
            public int AccessBy { get; set; }
            public ICollection<TransferItemDto> TransferItems { get; set; }

            public class TransferItemDto
            {
                public string ItemCode { get; set; }
                public decimal? Quantity { get; set; }
                public string Reason { get; set; }
                public decimal Amount { get; set; }
                public string Bbd { get; set; }

            }
        }

        public class Handler : IRequestHandler<AddTransferInCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddTransferInCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .Where(u => u.Id == request.AccessBy)
                    .FirstOrDefaultAsync(cancellationToken);

                var userTo = await _context.Users
                    .Include(u => u.UserRoles)
                    .Where(u => u.Id == request.To)
                    .FirstOrDefaultAsync(cancellationToken);

                bool isDepot = false;

                if (userTo.UserRoles.UserRoleName == Roles.Depot)
                {
                    isDepot = true;

                    var moveOrderItemsDepot = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == request.AccessBy &&
                                m.IsActive &&
                                m.RemainingQuantity > 0)
                    .OrderBy(m => m.MoveOrder.CreatedDate)
                    .ToListAsync(cancellationToken);

                    foreach (var transferItem in request.TransferItems)
                    {
                        var neededQty = transferItem.Quantity ?? 0;
                        if (neededQty <= 0) continue;

                        var matchingMoveOrderItems = moveOrderItemsDepot
                            .Where(x => x.ItemCode == transferItem.ItemCode && x.RemainingQuantity > 0);

                        foreach (var moveOrderItem in matchingMoveOrderItems)
                        {
                            if (moveOrderItem.RemainingQuantity >= neededQty)
                            {
                                moveOrderItem.RemainingQuantity -= neededQty;
                                neededQty = 0;
                                break;
                            }
                            else
                            {
                                neededQty -= moveOrderItem.RemainingQuantity ?? 0;
                                moveOrderItem.RemainingQuantity = 0;
                            }
                        }
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }

                if (user.UserRoles.UserRoleName != Roles.Cdo) //CDO 
                {
                    return InventoryErrors.NotUserCdo();
                }


                if (request.To == request.AccessBy)
                {
                    return InventoryErrors.Self();
                }


                if (userTo.UserRoles.UserRoleName != Roles.Cdo && userTo.UserRoles.UserRoleName != Roles.Depot) //CDO and Depot
                {
                    return InventoryErrors.NotCdo();
                }


                var requestedItems = request.TransferItems
                    .GroupBy(i => i.ItemCode)
                    .Select(g => new
                    {
                        ItemCode = g.Key,
                        RequestedQuantity = g.Sum(x => x.Quantity ?? 0)
                    })
                    .ToList();

                

                var transferOrderItems = await _context.TransferOrderItems
                    .Where(t => t.TransferOrder.TransferToId == request.AccessBy &&
                                t.TransferOrder.Status == Status.Received &&
                                t.IsActive &&
                                t.RemainingQuantity > 0)
                    .OrderBy(t => t.TransferOrder.TransactionDate)
                    .ToListAsync(cancellationToken);

                var returnOrderItems = await _context.ReturnOrderItems
                    .Where(r => r.ReturnOrder.CreatedbyId == request.AccessBy &&
                                r.ReturnOrder.Status == Status.Received &&
                                r.IsActive &&
                                r.RemainingQuantity > 0)
                    .OrderBy(r => r.ReturnOrder.CreatedDate)
                    .ToListAsync(cancellationToken);

                var moveOrderItems = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == request.AccessBy &&
                                m.IsActive &&
                                m.RemainingQuantity > 0)
                    .OrderBy(m => m.MoveOrder.CreatedDate)
                    .ToListAsync(cancellationToken);

                //
                var transferOrderItemsDto = transferOrderItems
                    .Select(t => new
                    {
                        ItemId = _context.Items
                                .Where(i => i.ItemCode == t.ItemCode)
                                .Select(i => i.Id)
                                .FirstOrDefault(),
                        RemainingQuantity = t.RemainingQuantity ?? 0
                    })
                    .ToList();

                var returnOrderItemsDto = returnOrderItems
                    .Select(r => new
                    {
                        r.ItemId,
                        RemainingQuantity = r.RemainingQuantity
                    })
                    .ToList();

                var moveOrderItemsDto = moveOrderItems
                    .Select(m => new
                    {
                        ItemId = _context.Items
                                .Where(i => i.ItemCode == m.ItemCode)
                                .Select(i => i.Id)
                                .FirstOrDefault(),
                        RemainingQuantity = m.RemainingQuantity ?? 0
                    })
                    .ToList();

                var combinedStock = transferOrderItemsDto
                    .Concat(returnOrderItemsDto)
                    .Concat(moveOrderItemsDto);

                var availableStockByItemId = combinedStock
                    .GroupBy(s => s.ItemId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(x => x.RemainingQuantity)
                    );
                //

                var itemCodeToId = await _context.Items
                    .Where(x => requestedItems.Select(r => r.ItemCode).Contains(x.ItemCode))
                    .ToDictionaryAsync(x => x.ItemCode, x => x.Id, cancellationToken); 
                await _context.SaveChangesAsync(cancellationToken);

                var transferOrder = new TransferOrder
                {
                    TransferToId = request.To,
                    TransactionType = Status.Transfer,
                    TotalQuantity = request.TransferItems.Sum(i => i.Quantity ?? 0),
                    TransactionDate = DateTime.Now,
                    TransferType = isDepot ? Status.Outright : Status.TransferOut,
                    CreatedById = request.AccessBy,
                    Status = isDepot ? Status.Received : Status.ForReceiving,
                    TotalAmount = request.TransferItems.Sum(i => (i.Amount * i.Quantity) ?? 0)
                };

                _context.TransferOrders.Add(transferOrder);
                await _context.SaveChangesAsync(cancellationToken);

                var itemCodes = requestedItems.Select(i => i.ItemCode).ToList();

                var moveOrderItemsForUser = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == request.AccessBy && itemCodes.Contains(m.ItemCode))
                    .OrderBy(m => m.ItemCode)
                    .ToListAsync(cancellationToken);

                var itemsInContext = await _context.Items
                    .Where(i => itemCodes.Contains(i.ItemCode))
                    .Select(i => new
                    {
                        i.Id,
                        i.ItemCode,
                        i.ItemDescription,
                        UomDescription = i.Uom.UomDescription
                    })
                    .ToListAsync(cancellationToken);

                var userFullMoveOrderItems = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == request.AccessBy && itemCodes.Contains(m.ItemCode))
                    .Select(mo => new
                    {
                        mo.ItemCode,
                        mo.ProductionDate,
                        MoveOrderExternal = mo.MoveOrder.MoveOrderIdExternal
                    })
                    .ToListAsync(cancellationToken);

                transferOrderItems = request.TransferItems.Select(i =>
                {
                    var matchedItem = itemsInContext.FirstOrDefault(x => x.ItemCode == i.ItemCode);
                    var matchedMoveOrderItem = userFullMoveOrderItems.FirstOrDefault(x => x.ItemCode == i.ItemCode);

                    return new TransferOrderItem
                    {
                        ItemCode = i.ItemCode,
                        ItemDescription = matchedItem?.ItemDescription,
                        Uom = matchedItem?.UomDescription,
                        Quantity = i.Quantity,
                        ProductionDate = matchedMoveOrderItem?.ProductionDate,
                        MoveId = matchedMoveOrderItem?.MoveOrderExternal,
                        TransferOrderId = transferOrder.Id,
                        CreatedById = request.AccessBy,
                        RemainingQuantity = i.Quantity,
                        ItemId = matchedItem?.Id ?? 0,
                        Reason = i.Reason,
                        Amount = i.Amount,
                        Bbd = i.Bbd,
                    };
                }).ToList();



                _context.TransferOrderItems.AddRange(transferOrderItems);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
