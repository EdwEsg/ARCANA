using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/update-bbd-v2"), ApiController]
    public class UpdateItemBbdV2 : ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateItemBbdV2(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateItemBbdV2Command command)
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

        public class UpdateItemBbdV2Command : IRequest<Result>
        {
            public int AccessBy { get; set; }
            public int? ClientId { get; set; }
            public List<ItemDto> Item { get; set; }
            public class ItemDto
            {
                public string ItemCode { get; set; }
                public List<BbdsDto> Bbds { get; set; }
                public class BbdsDto
                {
                    //public int? BbdId { get; set; }
                    public decimal Quantity { get; set; }
                    public DateTime? BbdDate { get; set; }
                }
            }
        }

        public class Handler : IRequestHandler<UpdateItemBbdV2Command, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateItemBbdV2Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                foreach (var item in request.Item)
                {
                    foreach (var bbd in item.Bbds)
                    {
                        if (bbd.Quantity > 0 && bbd.BbdDate is null)
                        {
                            return InventoryErrors.BbdDateNull(bbd.Quantity);
                        }
                    }
                }

                var itemGroup = request.Item
                    .GroupBy(x => x.ItemCode)
                    .Select(g => new
                    {
                        g.Key,
                    }).ToList();

                foreach (var itemCode in  itemGroup.OrderBy(x => x.Key))
                {
                    var itemBbd = _context.TransactionItemBbd
                        .Where(r => r.RemainingQuantity > 0 &&
                            r.ClientsId == request.ClientId &&
                            r.ItemCode == itemCode.Key);

                    foreach (var item in itemBbd)
                    {
                        item.RemainingQuantity = 0;
                    }
                }

                foreach (var item in request.Item)
                {
                    var items = await _context.TransactionItems
                        .Where(ti => ti.Item.ItemCode == item.ItemCode &&
                                ti.RemainingQuantity > 0 && 
                                ti.IsActive)
                        .SumAsync(ti => ti.RemainingQuantity, cancellationToken);

                    var inputItems = item.Bbds
                            .Sum(i => i.Quantity);

                    if (inputItems > items)
                    {
                        return InventoryErrors.ExcessQuantity(inputItems, items, item.ItemCode);
                    }

                    bool flagNoBbdId = false;
                    foreach (var bbd in item.Bbds/*.OrderBy(b => b.BbdId is null)*/)
                    {
                        
                        //if (bbd.BbdId is null) 
                        //{
                            var newItemBbd = new TransactionItemBbd
                            {
                                ClientsId = request.ClientId,
                                Quantity = bbd.Quantity,
                                RemainingQuantity = bbd.Quantity,
                                Bbd = bbd?.BbdDate,
                                CreatedDate = DateTime.Now,
                                ItemCode = item.ItemCode
                            };
                            await _context.TransactionItemBbd.AddAsync(newItemBbd, cancellationToken);

                            var transactionItems = await _context.TransactionItems
                                .Where(ti => ti.Item.ItemCode == item.ItemCode &&
                                ti.RemainingQuantity > 0 &&
                                ti.Transaction.ClientId == request.ClientId &&
                                ti.IsActive)
                                .OrderBy(ti => ti.CreatedAt)
                                .ToListAsync(cancellationToken);

                            decimal quantityHolder = 0;
                            foreach (var tranItem in transactionItems)
                            {
                                if (bbd.Quantity == 0 && bbd.BbdDate is not null) break;

                                if (tranItem.Quantity == tranItem.RemainingQuantity && quantityHolder == 0)
                                {
                                    tranItem.RemainingQuantity = bbd.Quantity;
                                    flagNoBbdId = true;
                                    break;
                                }

                                ////test
                                //else if (bbd.Quantity >= tranItem.RemainingQuantity && flagNoBbdId == false)
                                //{
                                //    quantityHolder = bbd.Quantity - tranItem.RemainingQuantity;
                                //    tranItem.RemainingQuantity = 0;
                                //    continue;
                                //}

                                //else if (quantityHolder is not 0)
                                //{
                                //    tranItem.RemainingQuantity = tranItem.Quantity - quantityHolder;
                                //    quantityHolder = 0;
                                //}

                                //else
                                //{
                                //    tranItem.RemainingQuantity += bbd.Quantity;
                                //}
                            }


                        //}

                        //else
                        //{
                        //    var bbdId = await _context.TransactionItemBbd
                        //        .FirstOrDefaultAsync(tib => tib.Id == bbd.BbdId, cancellationToken);

                        //    if (bbdId is null)
                        //    {
                        //        return InventoryErrors.CannotFoundBbd(bbd.BbdId);
                        //    }


                        //    var deductToTranItem = bbdId.RemainingQuantity - bbd.Quantity;
                        //    bbdId.RemainingQuantity = bbd.Quantity;

                        //    var transactionItems = await _context.TransactionItems
                        //        .Where(ti => ti.Item.ItemCode == item.ItemCode &&
                        //        ti.RemainingQuantity > 0 &&
                        //        ti.IsActive)
                        //        .OrderBy(ti => ti.CreatedAt)
                        //        .ToListAsync(cancellationToken);

                        //    foreach (var tranItem in transactionItems)
                        //    {
                        //        if (deductToTranItem == 0) break;

                        //        if (tranItem.RemainingQuantity >= deductToTranItem)
                        //        {
                        //            tranItem.RemainingQuantity -= deductToTranItem;
                        //            deductToTranItem = 0;
                        //        }
                        //        else
                        //        {
                        //            deductToTranItem -= tranItem.RemainingQuantity;
                        //            tranItem.RemainingQuantity = 0;
                        //        }
                        //    }
                        //    await _context.SaveChangesAsync(cancellationToken);
                        //}

                        await _context.SaveChangesAsync(cancellationToken);

                    }
                }

                return Result.Success();
            }
        }
    }
}
