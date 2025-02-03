using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/update-callsheet-items"), ApiController]
    public class UpdateRemainingInventoryInCallsheet : ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateRemainingInventoryInCallsheet(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{transactionId:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateRemainingInventoryInCallsheetCommand command, [FromRoute] int transactionId)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AccessBy = userId;
                }

                command.TransactionId = transactionId;
                var result = await _mediator.Send(command);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class UpdateRemainingInventoryInCallsheetCommand : IRequest<Result>
        {
            public int AccessBy { get; set; }
            public int TransactionId { get; set; }
            public ICollection<TransactionItemDtoForCallSheet> TransactionItems { get; set; }
            public class TransactionItemDtoForCallSheet
            {
                public int TransactionItemsId { get; set; }
                public decimal RemainingInv { get; set; }

            }
        }

        public class Handler : IRequestHandler<UpdateRemainingInventoryInCallsheetCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateRemainingInventoryInCallsheetCommand request, CancellationToken cancellationToken)
            {
                var userCdo = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                var transaction = await _context.Transactions
                    .Include(t => t.TransactionItems)
                    .Include(u => u.AddedByUser)
                    .FirstOrDefaultAsync(t => t.Id == request.TransactionId && t.AddedBy ==  request.AccessBy, cancellationToken);

                if (transaction == null)
                {
                    return InventoryErrors.TransactionNotFound(request.TransactionId.ToString(), userCdo.Fullname);
                }

                foreach (var itemDto in request.TransactionItems)
                {
                    var transactionItem = transaction.TransactionItems
                        .FirstOrDefault(ti => ti.Id == itemDto.TransactionItemsId);

                    if (transactionItem == null)
                    {
                        return InventoryErrors.TransactionItemNotFound(itemDto.TransactionItemsId.ToString(), userCdo.Fullname);
                    }

                    if (itemDto.RemainingInv > transactionItem.Quantity)
                    {
                        return InventoryErrors
                            .InvalidRemainingInventory(itemDto.RemainingInv, transactionItem.Quantity, transactionItem.Id.ToString());
                    }
                }


                foreach (var itemDto in request.TransactionItems)
                {
                    var transactionItem = transaction.TransactionItems
                        .First(ti => ti.Id == itemDto.TransactionItemsId);

                    transactionItem.RemainingQuantity = itemDto.RemainingInv;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}
