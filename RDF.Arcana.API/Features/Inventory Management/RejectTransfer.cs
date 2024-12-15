using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/reject-transfer"), ApiController]
    public class RejectTransfer : ControllerBase
    {
        private readonly IMediator _mediator;
        public RejectTransfer(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] RejectTransferCommand command)
        {
            try
            {
                command.TransferId = id;

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

        public class RejectTransferCommand : IRequest<Result>
        {
            public int TransferId { get; set; }
            public int AccessBy { get; set; }
        }

        public class RejectTransferHandler : IRequestHandler<RejectTransferCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public RejectTransferHandler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(RejectTransferCommand request, CancellationToken cancellationToken)
            {
                var transferOrder = await _context.TransferOrders
                    .FirstOrDefaultAsync(to => to.Id == request.TransferId, cancellationToken);

                if (transferOrder == null)
                {
                    return InventoryErrors.ToNotFound();
                }

                transferOrder.Status = Status.Rejected;
                transferOrder.ModifiedBy = request.AccessBy;
                transferOrder.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
