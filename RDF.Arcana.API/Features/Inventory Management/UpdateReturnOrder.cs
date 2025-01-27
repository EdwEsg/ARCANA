
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/update-return-order"), ApiController]
    public class UpdateReturnOrder : ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateReturnOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{returnOrderId:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateReturnOrderCommand command, [FromRoute] int returnOrderId)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AccessBy = userId;
                }

                command.ReturnOrderId = returnOrderId;
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

        public class UpdateReturnOrderCommand : IRequest<Result>
        {
            public int ReturnOrderId { get; set; }
            public int AccessBy { get; set; }
            public List<ReturnItemsDto> ReturnItems { get; set; }
            public class ReturnItemsDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public string Bbd { get; set; }
                public string Reason { get; set; }
            }
        }

        public class Handler : IRequestHandler<UpdateReturnOrderCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateReturnOrderCommand request, CancellationToken cancellationToken)
            {
                var returnedOrder = await _context.ReturnedOrders
                    .Include(ro => ro.ReturnOrderItems)
                    .FirstOrDefaultAsync(ro => ro.Id == request.ReturnOrderId, cancellationToken);

                if (returnedOrder == null)
                    return InventoryErrors.NoReturnFound();

                if (returnedOrder.Status != Status.Pending)
                    return InventoryErrors.InvalidStatus();

                if (returnedOrder.CreatedbyId != request.AccessBy)
                    return InventoryErrors.Unauthorized();

                var oldItems = returnedOrder.ReturnOrderItems.ToList();
                _context.ReturnOrderItems.RemoveRange(oldItems);

                var newReturnItems = request.ReturnItems.Select(x => new ReturnOrderItem
                {
                    ReturnOrderId = returnedOrder.Id,
                    ItemId = x.ItemId,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Bbd = x.Bbd,
                    Reason = x.Reason,
                    RemainingQuantity = x.Quantity,
                    IsActive = true
                });

                await _context.ReturnOrderItems.AddRangeAsync(newReturnItems, cancellationToken);

                var newTotalReturn = newReturnItems.Sum(i => i.Price * i.Quantity);
                returnedOrder.TotalReturn = newTotalReturn;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
