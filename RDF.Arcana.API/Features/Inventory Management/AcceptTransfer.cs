
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/accept-transfer"), ApiController]
    public class AcceptTransfer : ControllerBase
    {
        private readonly IMediator _mediator;
        public AcceptTransfer(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] AddTransferInCommand2 command)
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
                    .FirstOrDefaultAsync(to => to.Id == request.TransferId, cancellationToken);

                if (transferOrders == null)
                {
                    return InventoryErrors.ToNotFound();
                }

                var senderId = transferOrders.CreatedById;

                var senderTransferStocks = await _context.TransferOrderItems
                    .Where(t => t.TransferOrder.TransferToId == senderId &&
                                t.TransferOrder.Status == Status.Received &&
                                t.IsActive &&
                                t.RemainingQuantity > 0)
                    .OrderBy(t => t.TransferOrder.TransactionDate)
                    .ToListAsync(cancellationToken);

                var senderReturnStocks = await _context.ReturnOrderItems
                    .Where(r => r.ReturnOrder.CreatedbyId == senderId &&
                                r.ReturnOrder.Status == Status.Received &&
                                r.IsActive &&
                                r.RemainingQuantity > 0)
                    .OrderBy(r => r.ReturnOrder.CreatedDate)
                    .ToListAsync(cancellationToken);

                var senderMoveStocks = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == senderId &&
                                m.IsActive &&
                                m.RemainingQuantity > 0)
                    .OrderBy(m => m.MoveOrder.CreatedDate)
                    .ToListAsync(cancellationToken);

                var transferOrderItemsList = await _context.TransferOrderItems
                    .Where(t => t.TransferOrderId == transferOrders.Id)
                    .ToListAsync(cancellationToken);

                foreach (var orderItem in transferOrderItemsList)
                {
                    var itemCode = orderItem.ItemCode;
                    var remainingToDeduct = orderItem.Quantity;

                    foreach (var stock in senderTransferStocks.Where(s => s.ItemCode == itemCode))
                    {
                        if (remainingToDeduct <= 0) break;
                        var available = stock.RemainingQuantity ?? 0;
                        var deduction = available >= remainingToDeduct ? remainingToDeduct : available;
                        stock.RemainingQuantity = (stock.RemainingQuantity ?? 0) - deduction;
                        remainingToDeduct -= deduction;
                    }

                    foreach (var stock in senderReturnStocks.Where(s => s.ItemId == orderItem.ItemId))
                    {
                        if (remainingToDeduct <= 0) break;
                        var available = stock.RemainingQuantity;
                        var deduction = available >= remainingToDeduct ? remainingToDeduct : available;
                        stock.RemainingQuantity = (available - deduction) ?? 0;
                        remainingToDeduct -= deduction;
                    }

                    foreach (var stock in senderMoveStocks.Where(s => s.ItemCode == itemCode))
                    {
                        if (remainingToDeduct <= 0) break;
                        var available = stock.RemainingQuantity ?? 0;
                        var deduction = available >= remainingToDeduct ? remainingToDeduct : available;
                        stock.RemainingQuantity = (stock.RemainingQuantity ?? 0) - deduction;
                        remainingToDeduct -= deduction;
                    }

                    if (remainingToDeduct > 0)
                    {
                        throw new InvalidOperationException($"Unable to fully deduct quantity for ItemCode '{itemCode}'. Remaining: {remainingToDeduct}");
                    }
                }


                transferOrders.Status = Status.Received;
                transferOrders.ModifiedBy = request.AccessBy;
                transferOrders.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
