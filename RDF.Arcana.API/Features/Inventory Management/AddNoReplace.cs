using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-no-replace"), ApiController]
    public class AddNoReplace : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddNoReplace(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddNoReplaceCommand command)
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

        public class AddNoReplaceCommand : IRequest<Result>
        {
            public int ReturnOrderId { get; set; }
            public decimal Amount { get; set; }
            public int AccessBy { get; set; }
        }

        public class Handler : IRequestHandler<AddNoReplaceCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddNoReplaceCommand request, CancellationToken cancellationToken)
            {
                bool isUserCdo = await _context.Users
                    .Include(u => u.UserRoles)
                    .Where(u => u.Id == request.AccessBy)
                    .Select(u => u.UserRoles.UserRoleName)
                    .FirstOrDefaultAsync(cancellationToken) == Roles.Cdo;
                if (!isUserCdo)
                {
                    return InventoryErrors.NotUserCdo();
                }

                var isReturnOrderExist = await _context.ReturnedOrders
                    .FirstOrDefaultAsync(r => r.Id == request.ReturnOrderId, cancellationToken);
                if (isReturnOrderExist is null)
                {
                    return InventoryErrors.ReturnOrderIdNotFound(request.ReturnOrderId.ToString());
                }

                isReturnOrderExist.Status = Status.Received;

                var salesReturn = new SalesReturn
                {
                    ReturnedOrderId = request.ReturnOrderId,
                    ClientId = isReturnOrderExist.ClientId,
                    Amount = request.Amount,
                    RemainingBalance = request.Amount,
                    CreatedById = request.AccessBy,
                };

                await _context.AddAsync(salesReturn);
                    

                await _context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}
