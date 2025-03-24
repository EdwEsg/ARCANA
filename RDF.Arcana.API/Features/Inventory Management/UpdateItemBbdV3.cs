using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/update-bbd-v3"), ApiController]
    public class UpdateItemBbdV3 : ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateItemBbdV3(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateItemBbdV3Command command)
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

        public class UpdateItemBbdV3Command : IRequest<Result>
        {
            public int AccessBy { get; set; }
            public int ClientId { get; set; }
            public List<ItemDtoo> Item { get; set; }
            public class ItemDtoo
            {
                public string ItemCode { get; set; }
                public List<BbdDto> Bbds { get; set; }
                public class BbdDto
                {
                    public decimal Quantity { get; set; }
                    public DateTime? BbdDate { get; set; }
                }
            }
        }

        public class Handler : IRequestHandler<UpdateItemBbdV3Command, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateItemBbdV3Command request, CancellationToken cancellationToken)
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

                foreach (var itemCode in itemGroup.OrderBy(x => x.Key))
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
                await _context.SaveChangesAsync(cancellationToken);

                foreach (var item in request.Item)
                {
                    var inputItems = item.Bbds
                            .Sum(i => i.Quantity);

                    foreach (var bbd in item.Bbds)
                    {
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

                        for (int i = 0; i < transactionItems.Count; i++) 
                        {
                            if (i == transactionItems.Count - 1) 
                            {
                                transactionItems[i].RemainingQuantity = inputItems; 
                            }
                            else
                            {
                                transactionItems[i].RemainingQuantity = 0; 
                            }
                        }

                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }

                return Result.Success();
            }
        }
    }
}
