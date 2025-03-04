using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/delete-callsheet-bbd")]
    public class DeleteCallSheetBbd : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeleteCallSheetBbd(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{bbdId:int}")]
        public async Task<IActionResult> Delete([FromRoute]int bbdId, [FromBody] DeleteCallSheetBbdCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AccessBy = userId;
                }

                command.BbdId = bbdId;
                var result = await _mediator.Send(command);
                if (result.IsFailure)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class DeleteCallSheetBbdCommand : IRequest<Result>
        {
            public int AccessBy { get; set; }
            public int BbdId { get; set; }
        }

        public class Handler : IRequestHandler<DeleteCallSheetBbdCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(DeleteCallSheetBbdCommand request, CancellationToken cancellationToken)
            {
                var currentUser = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);
                if (currentUser?.UserRoles?.UserRoleName != Roles.Cdo)
                {
                    return InventoryErrors.NotCdo();
                }

                var bbdId = await _context.TransactionItemBbd
                    .Include(ti => ti.TransactionItems)
                    .FirstOrDefaultAsync(b => b.Id == request.BbdId, cancellationToken);

                if (bbdId == null)
                {
                    return InventoryErrors.NotFound(request.BbdId.ToString());
                }

                bbdId.IsActive = false;
                bbdId.TransactionItems.RemainingQuantity += bbdId.Quantity;
                bbdId.TransactionItems.IsActive = true;
                await _context.SaveChangesAsync(cancellationToken);

                bbdId.TransactionItems.RemainingQuantity -= bbdId.RemainingQuantity;
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
