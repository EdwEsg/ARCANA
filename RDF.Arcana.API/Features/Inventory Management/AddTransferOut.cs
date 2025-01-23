
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

                var userMoveOrderItems = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == request.AccessBy)
                    .GroupBy(m => m.ItemCode)
                    .Select(g => new
                    {
                        ItemCode = g.Key,
                        AvailableQuantity = g.Sum(x => x.RemainingQuantity ?? 0)
                    })
                    .ToListAsync(cancellationToken);

                //var userTransferInItems = await _context.TransferOrderItems
                //    .Where(t => t.TransferOrder.TransferToId == request.AccessBy)
                //    .GroupBy(t => t.ItemCode)
                //    .Select(g => new
                //    {
                //        ItemCode = g.Key,
                //        AvailableQuantity = g.Sum(x => x.RemainingQuantity ?? 0)
                //    })
                //    .ToListAsync(cancellationToken);

                //var totalAvailableQuantity = userMoveOrderItems
                //    .Concat(userTransferInItems)
                //    .GroupBy(x => x.ItemCode)
                //    .Select(g => new
                //    {
                //        ItemCode = g.Key,
                //        AvailableQuantity = g.Sum(i => i.AvailableQuantity)
                //    })
                //    .ToList();

                foreach (var reqItem in requestedItems)
                {
                    var matchingUserItem = userMoveOrderItems.FirstOrDefault(u => u.ItemCode == reqItem.ItemCode);

                    if (matchingUserItem == null)
                    {
                        return InventoryErrors.ItemNotFound(reqItem.ItemCode);
                    }

                    if (reqItem.RequestedQuantity > matchingUserItem.AvailableQuantity)
                    {
                        return InventoryErrors.InsufficientQuantity(
                            reqItem.ItemCode,
                            reqItem.RequestedQuantity,
                            matchingUserItem.AvailableQuantity
                        );
                    }
                }

                var itemCodes = requestedItems.Select(i => i.ItemCode).ToList();

                var moveOrderItemsForUser = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == request.AccessBy && itemCodes.Contains(m.ItemCode))
                    .OrderBy(m => m.ItemCode) 
                    .ToListAsync(cancellationToken);

                //var transferInItemsForUser = await _context.TransferOrderItems
                //    .Where(t => itemCodes.Contains(t.ItemCode))
                //    .OrderBy(t => t.ItemCode)
                //    .ToListAsync(cancellationToken);


                foreach (var reqItem in requestedItems)
                {
                    
                    var matchedMoveOrderItems = moveOrderItemsForUser
                        .Where(m => m.ItemCode == reqItem.ItemCode)
                        .ToList();

                    var quantityToDeduct = reqItem.RequestedQuantity;

                    foreach (var moItem in matchedMoveOrderItems)
                    {
                        if (quantityToDeduct <= 0) break;

                        var available = moItem.RemainingQuantity ?? 0;
                        if (available >= quantityToDeduct)
                        {
                            moItem.RemainingQuantity = available - quantityToDeduct;
                            quantityToDeduct = 0;
                        }
                        else
                        {
                            moItem.RemainingQuantity = 0;
                            quantityToDeduct -= available;
                        }
                    }

                    //if (quantityToDeduct > 0)
                    //{
                    //    var matchedTransferInItems = transferInItemsForUser
                    //        .Where(t => t.ItemCode == reqItem.ItemCode)
                    //        .ToList();

                    //    foreach (var tiItem in matchedTransferInItems)
                    //    {
                    //        if (quantityToDeduct <= 0) break;

                    //        var available = tiItem.RemainingQuantity ?? 0;
                    //        if (available >= quantityToDeduct)
                    //        {
                    //            tiItem.RemainingQuantity = available - quantityToDeduct;
                    //            quantityToDeduct = 0;
                    //        }
                    //        else
                    //        {
                    //            tiItem.RemainingQuantity = 0;
                    //            quantityToDeduct -= available;
                    //        }
                    //    }
                    //}

                    if (quantityToDeduct > 0)
                    {
                        throw new InvalidOperationException($"Unable to fully deduct quantity for ItemCode '{reqItem.ItemCode}'. Remaining: {quantityToDeduct}");
                    }
                }
                
                await _context.SaveChangesAsync(cancellationToken);

                var transferOrder = new TransferOrder
                {
                    TransferToId = request.To,
                    TransactionType = Status.Transfer,
                    TotalQuantity = request.TransferItems.Sum(i => i.Quantity ?? 0),
                    TransactionDate = DateTime.Now,
                    TransferType = Status.TransferOut,
                    CreatedById = request.AccessBy,
                    Status = Status.ForReceiving,
                };

                _context.TransferOrders.Add(transferOrder);
                await _context.SaveChangesAsync(cancellationToken);

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

                var transferOrderItems = request.TransferItems.Select(i =>
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
                        Reason = i.Reason
                    };
                }).ToList();



                _context.TransferOrderItems.AddRange(transferOrderItems);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
