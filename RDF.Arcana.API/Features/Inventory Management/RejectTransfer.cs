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

                var transferOrderItems = await _context.TransferOrderItems
                    .Where(toi => toi.TransferOrderId == request.TransferId)
                    .ToListAsync(cancellationToken);

                if (!transferOrderItems.Any())
                {
                    return InventoryErrors.ToNotFound();
                }

                var groupedItems = transferOrderItems
                    .GroupBy(i => i.ItemCode)
                    .Select(g => new
                    {
                        ItemCode = g.Key,
                        QuantityToRestore = g.Sum(x => x.Quantity ?? 0)
                    })
                    .ToList();

                var moveOrderItemsCreator = transferOrder.CreatedById;

                var itemCodes = groupedItems.Select(i => i.ItemCode).ToList();
                var userMoveOrderItems = await _context.MoveOrderItems
                    .Where(m => m.CreatedBy.Id == moveOrderItemsCreator && itemCodes.Contains(m.ItemCode) 
                        && m.Reason == null)
                    .OrderBy(m => m.ItemCode)
                    .ToListAsync(cancellationToken);

                foreach (var item in groupedItems)
                {
                    var quantityToRestore = item.QuantityToRestore;

                    var matchedMoveOrderItems = userMoveOrderItems
                        .Where(m => m.ItemCode == item.ItemCode && m.Reason == null)
                        .ToList();

                    foreach (var moItem in matchedMoveOrderItems)
                    {
                        if (quantityToRestore <= 0)
                            break;

                        var currentQuantity = moItem.RemainingQuantity ?? 0;
                        var maxAllowed = moItem.Quantity; 
                        var availableSpace = maxAllowed - currentQuantity;

                        if (availableSpace <= 0)
                        {
                            continue;
                        }

                        if (quantityToRestore <= availableSpace)
                        {
                            moItem.RemainingQuantity = currentQuantity + quantityToRestore;
                            quantityToRestore = 0;
                        }
                        else
                        {
                            moItem.RemainingQuantity = currentQuantity + availableSpace;
                            quantityToRestore -= availableSpace;
                        }
                    }

                    if (quantityToRestore > 0)
                    {
                        throw new InvalidOperationException(
                            $"Unable to restore full quantity for ItemCode '{item.ItemCode}'. Missing {quantityToRestore} units."
                        );
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                transferOrder.Status = Status.Rejected;
                transferOrder.ModifiedBy = request.AccessBy;
                transferOrder.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
