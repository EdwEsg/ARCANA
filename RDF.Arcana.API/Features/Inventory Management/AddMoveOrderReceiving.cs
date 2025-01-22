
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using RDF.Arcana.API.Models.ExternalDb;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-move-order-receivings"), ApiController]
    public class AddMoveOrderReceiving : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddMoveOrderReceiving(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddMoveOrderReceivingCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.CreatedBy = userId;
                }
                var result = await _mediator.Send(command);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class AddMoveOrderReceivingCommand : IRequest<Result>
        {
            public int MoveOrderId { get; set; }
            public int CreatedBy { get; set; }
            public List<MoveOrderItemDto> Items { get; set; }
            public List<AdditionalDeliverDto> Additional { get; set; }
            public List<WrongDeliverDto> Wrong { get; set; }
            public class MoveOrderItemDto
            {
                public string ItemCode { get; set; }
                public decimal? ActualQuantity { get; set; }
            }

            public class AdditionalDeliverDto 
            {
                public string ItemCode { get; set; }
                public decimal? Quantity { get; set; }
                public string Reason { get; set; }

            }

            public class WrongDeliverDto
            {
                public string ItemCode { get; set; }
                public decimal? Quantity { get; set; }
                public string Reason { get; set; }
            }
        }

        public class Handler : IRequestHandler<AddMoveOrderReceivingCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            private readonly ExternalDbContext _external;

            public Handler(ArcanaDbContext context, ExternalDbContext external)
            {
                _context = context;
                _external = external;
            }

            public async Task<Result> Handle(AddMoveOrderReceivingCommand request, CancellationToken cancellationToken)
            {
                
                var externalMoveOrder = await (from mo in _external.MoveOrders
                                               join cust in _external.Customers on mo.CustomerId equals cust.Id into custGroup
                                               from cust in custGroup.DefaultIfEmpty()
                                               join area in _external.Areas on cust.AreaId equals area.Id into areaGroup
                                               from area in areaGroup.DefaultIfEmpty()
                                               where mo.Id == request.MoveOrderId && mo.TransactStatus == true
                                               select new
                                               {
                                                   mo.Id,
                                                   mo.CustomerId,
                                                   mo.Description,
                                                   mo.TransactionDate,
                                                   mo.DeliveryDate,
                                                   mo.TransactStatus,
                                                   CustomerName = cust != null ? cust.CustomerName : string.Empty,
                                                   Route = cust != null ? cust.Address : string.Empty,
                                                   Area = area != null ? area.Area1 : string.Empty
                                               }).FirstOrDefaultAsync(cancellationToken);

                if (externalMoveOrder == null)
                {
                    return InventoryErrors.NotYetTransacted();
                }

                
                bool moveOrderExists = await _context.MoveOrders
                    .AnyAsync(mo => mo.MoveOrderIdExternal == request.MoveOrderId, cancellationToken);

                if (moveOrderExists)
                {
                    return InventoryErrors.MoAlreadyExist();
                }

                
                var externalMoveOrderItems = await (from moi in _external.MoveOrderItems
                                                    where moi.MoveId == request.MoveOrderId
                                                    join rm in _external.RmMasterlists on moi.ItemId equals rm.Int
                                                    join uom in _external.Uoms on rm.UomId equals uom.Id into uomGroup
                                                    from uom in uomGroup.DefaultIfEmpty()
                                                    select new
                                                    {
                                                        ItemCode = rm.ItemCode,
                                                        ItemDescription = rm.ItemDescription,
                                                        UomDescription = uom != null ? uom.UomDescription : string.Empty,
                                                        Quantity = (decimal)(moi.Quantity ?? 0),
                                                        ProductionDate = moi.ProductionDate
                                                    })
                                                    .ToListAsync(cancellationToken);

                if (!externalMoveOrderItems.Any())
                {
                    return InventoryErrors.MoNotFound();
                }

                
                var itemCodes = externalMoveOrderItems.Select(i => i.ItemCode).Distinct().ToList();
                var itemsInContext = await _context.Items
                    .Where(i => itemCodes.Contains(i.ItemCode))
                    .ToListAsync(cancellationToken);

                
                var additionalCodes = request.Additional?.Select(a => a.ItemCode).Distinct().ToList() ?? new List<string>();
                var allItemCodes = itemCodes.Concat(additionalCodes).Distinct().ToList();

                
                itemsInContext = await _context.Items
                    .Where(i => allItemCodes.Contains(i.ItemCode))
                    .ToListAsync(cancellationToken);

                var missingItemCodes = allItemCodes.Except(itemsInContext.Select(i => i.ItemCode)).ToList();
                if (missingItemCodes.Any())
                {
                    var missingCodes = string.Join(", ", missingItemCodes);
                    return InventoryErrors.CannotSync(missingCodes);
                }

                
                var internalMoveOrder = new Domain.Inventory.MoveOrder
                {
                    CustomerName = externalMoveOrder.CustomerName,
                    Route = externalMoveOrder.Route,
                    Details = externalMoveOrder.Description ?? string.Empty,
                    Area = externalMoveOrder.Area,
                    TransactionDate = externalMoveOrder.TransactionDate,
                    DeliveryDate = externalMoveOrder.DeliveryDate,
                    MoveOrderIdExternal = externalMoveOrder.Id,
                    CreatedById = request.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                await _context.MoveOrders.AddAsync(internalMoveOrder, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                
                var internalMoveOrderItems = new List<Domain.Inventory.MoveOrderItem>();

                foreach (var externalItem in externalMoveOrderItems)
                {
                    
                    var item = itemsInContext.First(i => i.ItemCode == externalItem.ItemCode);

                    
                    var commandItem = request.Items?.FirstOrDefault(i => i.ItemCode == externalItem.ItemCode);
                    decimal actualQuantity = commandItem?.ActualQuantity ?? externalItem.Quantity;

                    

                    var moveOrderItem = new Domain.Inventory.MoveOrderItem
                    {
                        MoveOrderId = internalMoveOrder.Id,
                        ItemCode = externalItem.ItemCode,
                        Quantity = externalItem.Quantity,       
                        ActualQuantity = actualQuantity,        
                        ProductionDate = externalItem.ProductionDate,
                        ItemId = item.Id,
                        UomId = item.UomId,
                        IsActive = true,
                        CreatedBy = _context.Users.FirstOrDefault(u => u.Id == request.CreatedBy),
                        Reason = null,                          
                        RemainingQuantity = actualQuantity      
                    };

                    if (request.Wrong != null && request.Wrong.Any())
                    {
                        
                        var totalWrongForItem = request.Wrong
                            .Where(w => w.ItemCode == externalItem.ItemCode)
                            .Sum(w => w.Quantity ?? 0m);

                        
                        moveOrderItem.RemainingQuantity -= totalWrongForItem;
                        if (moveOrderItem.RemainingQuantity < 0m)
                        {
                            moveOrderItem.RemainingQuantity = 0m; 
                        }
                    }

                    internalMoveOrderItems.Add(moveOrderItem);
                }

                
                if (request.Additional != null)
                {
                    foreach (var additionalItem in request.Additional)
                    {
                        var item = itemsInContext.First(i => i.ItemCode == additionalItem.ItemCode);

                        var addQuantity = additionalItem.Quantity ?? 0m;

                        
                        var additionalMoveOrderItem = new Domain.Inventory.MoveOrderItem
                        {
                            MoveOrderId = internalMoveOrder.Id,
                            ItemCode = additionalItem.ItemCode,
                            Quantity = addQuantity,
                            ActualQuantity = addQuantity,         
                            ProductionDate = null,                
                            ItemId = item.Id,
                            UomId = item.UomId,
                            IsActive = true,
                            CreatedBy = _context.Users.FirstOrDefault(u => u.Id == request.CreatedBy),
                            Reason = additionalItem.Reason,       
                            RemainingQuantity = addQuantity
                        };
                        internalMoveOrderItems.Add(additionalMoveOrderItem);
                    }
                }

                if (request.Wrong != null && request.Wrong.Any())
                {
                    foreach (var wrongItem in request.Wrong)
                    {
                        
                        var item = itemsInContext.First(i => i.ItemCode == wrongItem.ItemCode);

                        var wrongMoveOrderItem = new Domain.Inventory.MoveOrderItem
                        {
                            MoveOrderId = internalMoveOrder.Id,
                            ItemCode = wrongItem.ItemCode,
                            Quantity = wrongItem.Quantity ?? 0m,
                            ActualQuantity = null,
                            ProductionDate = null,
                            ItemId = item.Id,
                            UomId = item.UomId,
                            IsActive = true,
                            CreatedBy = _context.Users.FirstOrDefault(u => u.Id == request.CreatedBy),
                            Reason = wrongItem.Reason,
                            RemainingQuantity = null
                        };
                        internalMoveOrderItems.Add(wrongMoveOrderItem);
                    }
                }

                await _context.MoveOrderItems.AddRangeAsync(internalMoveOrderItems, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }

    }
}
