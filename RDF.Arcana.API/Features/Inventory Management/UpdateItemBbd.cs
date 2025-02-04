using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/update-bbd"), ApiController]
    public class UpdateItemBbd : ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateItemBbd(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{itemId:int}")]
        public async Task<IActionResult> Put([FromBody] UpdateItemBbdCommand command, [FromRoute] int itemId)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AccessBy = userId;
                }

                command.TransactionItemId = itemId;
                var result = await _mediator.Send(command);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class UpdateItemBbdCommand : IRequest<Result>
        {
            public int TransactionItemId { get; set; }
            public int AccessBy { get; set; }
            public List<ItemBbdDto> itemBbds { get; set; }
            public class ItemBbdDto
            {
                public decimal Quantity { get; set; }
                public DateTime Bbd { get; set; }
            }
        }

        public class BbdHandler : IRequestHandler<UpdateItemBbdCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public BbdHandler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateItemBbdCommand request, CancellationToken cancellationToken)
            {
                var userCdo = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                var transactionItem = await _context.TransactionItems
                    .Include(t => t.Transaction)
                    .Include(bbd => bbd.TransactionItemBbd)
                    .FirstOrDefaultAsync(ti => ti.Id == request.TransactionItemId && ti.Transaction.AddedBy == request.AccessBy, cancellationToken);

                if (transactionItem == null)
                {
                    return InventoryErrors.TransactionItemNotFound(request.TransactionItemId.ToString(), userCdo.Fullname);
                }

                decimal sumOfBbdQuantity = request.itemBbds.Sum(t => t.Quantity);
                if (sumOfBbdQuantity > transactionItem.RemainingQuantity)
                {
                    return InventoryErrors.InvalidRemainingInventory(transactionItem.RemainingQuantity, sumOfBbdQuantity, transactionItem.Id.ToString());
                }

                var newBbds = request.itemBbds.Select(x => new TransactionItemBbd
                {
                    TransactionItemsId = transactionItem.Id,
                    Quantity = x.Quantity,
                    Bbd = x.Bbd
                }).ToList();

                await _context.TransactionItemBbd.AddRangeAsync(newBbds, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}