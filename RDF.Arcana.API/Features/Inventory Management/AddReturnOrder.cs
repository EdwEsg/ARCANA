using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-return-order"), ApiController]
    public class AddReturnOrder : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddReturnOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddReturnOrderCommand command)
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

        public class AddReturnOrderCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public int AccessBy { get; set; }
            public ICollection<ReturnOrdersDto> ReturnOrders { get; set; }
            public class ReturnOrdersDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public string Bbd { get; set; }
                public string Reason { get; set; }
            }

        }

        public class Handler : IRequestHandler<AddReturnOrderCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddReturnOrderCommand request, CancellationToken cancellationToken)
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

                bool isClientExist = await _context.Clients
                    .AnyAsync(c => c.Id == request.ClientId);

                if (!isClientExist)
                {
                    return InventoryErrors.NoClientFound();
                }


                decimal totalReturn = request.ReturnOrders.Sum(x => x.Price * x.Quantity);

                var returnedOrder = new ReturnedOrder
                {
                    ClientId = request.ClientId,
                    TotalReturn = totalReturn,
                    TotalExchange = 0, 
                    CreatedbyId = request.AccessBy,
                    CreatedDate = DateTime.Now,
                    Status = Status.Pending  
                };

                _context.ReturnedOrders.Add(returnedOrder);
                await _context.SaveChangesAsync(cancellationToken);

                
                var returnOrderItems = request.ReturnOrders.Select(r => new ReturnOrderItem
                {
                    ReturnOrderId = returnedOrder.Id,
                    ItemId = r.ItemId,
                    Quantity = r.Quantity,
                    Price = r.Price,
                    Bbd = r.Bbd,
                    RemainingQuantity = r.Quantity,
                    Reason = r.Reason
                }).ToList();

                _context.ReturnOrderItems.AddRange(returnOrderItems);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}
