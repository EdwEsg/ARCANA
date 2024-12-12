
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-transfer-in"), ApiController]
    public class AddTransferIn : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddTransferIn(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] AddTransferInCommand2 command)
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

        public class AddTransferInCommand2 : IRequest<Result>
        {
            public int TransferId { get; set; }
            public int AccessBy { get; set; }
        }

        public class Handler : IRequestHandler<AddTransferInCommand2, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddTransferInCommand2 request, CancellationToken cancellationToken)
            {
                var transferOrders = await _context.TransferOrders
                    .Where(to => to.Id == request.TransferId && 
                        to.To == request.AccessBy &&
                        to.Status != Status.Received)
                    .FirstOrDefaultAsync(cancellationToken);

                if (transferOrders == null)
                {
                    return InventoryErrors.ToNotFound();
                }

                transferOrders.Status = Status.Received;
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
