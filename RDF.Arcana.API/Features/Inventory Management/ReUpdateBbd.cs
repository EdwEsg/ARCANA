
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/reupdate-bbd"), ApiController]
    public class ReUpdateBbd : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReUpdateBbd(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ReUpdateBbdCommand command)
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

        public class ReUpdateBbdCommand : IRequest<Result>
        {
            public int AccessBy { get; set; }
            public ICollection<BbdPerItem> bbdPerItem { get; set; }
            public class BbdPerItem
            {
                public int BbdId { get; set; }
                public decimal Quantity { get; set; }
                public DateTime Bbd { get; set; }
            }
            
        }

        public class Handler : IRequestHandler<ReUpdateBbdCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(ReUpdateBbdCommand request, CancellationToken cancellationToken)
            {
                var userCdo = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                foreach (var bbdItem in request.bbdPerItem)
                {
                    var transactionItemBbd = await _context.TransactionItemBbd
                        .Include(b => b.TransactionItems)
                        .FirstOrDefaultAsync(b => b.Id == bbdItem.BbdId, cancellationToken);

                    if (transactionItemBbd == null)
                    {
                        return InventoryErrors.TransactionItemNotFound(bbdItem.BbdId.ToString(), userCdo.Fullname);
                    }

                    transactionItemBbd.Quantity = bbdItem.Quantity;
                    transactionItemBbd.Bbd = bbdItem.Bbd;

                    if (transactionItemBbd.TransactionItems != null)
                    {
                        var totalQuantity = await _context.TransactionItemBbd
                            .Where(b => b.TransactionItemsId == transactionItemBbd.TransactionItems.Id && b.IsActive)
                            .SumAsync(b => b.Quantity, cancellationToken);
                        transactionItemBbd.TransactionItems.RemainingQuantity = totalQuantity;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
