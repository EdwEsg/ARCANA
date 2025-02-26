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

        [HttpPut("{transactionItemId:int}")]
        public async Task<IActionResult> Put([FromBody] UpdateItemBbdCommand command, [FromRoute] int transactionItemId)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AccessBy = userId;
                }

                command.TransactionItemId = transactionItemId;
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
            public List<ItemBbdDto> ItemBbds { get; set; }
            public class ItemBbdDto
            {
                public int? BbdId { get; set; }
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
                    .Include(t => t.TransactionItemBbd)
                    .FirstOrDefaultAsync(
                        ti => ti.Id == request.TransactionItemId && ti.Transaction.AddedBy == request.AccessBy,
                        cancellationToken);

                if (transactionItem == null)
                {
                    return InventoryErrors.TransactionItemNotFound(request.TransactionItemId.ToString(), userCdo.Fullname);
                }

                decimal sumOfIncomingQuantities = request.ItemBbds.Sum(x => x.Quantity);
                if (sumOfIncomingQuantities > transactionItem.Quantity)
                {
                    return InventoryErrors.InvalidRemainingInventory(
                        transactionItem.Quantity,
                        sumOfIncomingQuantities,
                        transactionItem.Id.ToString());
                }

                var originalBbdItems = transactionItem.TransactionItemBbd.Where(x => x.IsActive).ToList();

                foreach (var item in request.ItemBbds)
                {
                    if (item.BbdId.HasValue && item.BbdId.Value > 0)
                    {
                        var existingBbd = await _context.TransactionItemBbd.Where(x => x.IsActive)
                            .FirstOrDefaultAsync(
                                b => b.Id == item.BbdId.Value && b.TransactionItemsId == transactionItem.Id && b.IsActive,
                                cancellationToken);

                        if (existingBbd == null)
                        {
                            return InventoryErrors.TransactionItemNotFound(item.BbdId.Value.ToString(), userCdo.Fullname);
                        }

                        if (existingBbd.Quantity < item.Quantity)
                        {
                            return InventoryErrors.InvalidRemainingInventory(item.Quantity,existingBbd.Quantity, existingBbd.Id.ToString());
                        }

                        existingBbd.RemainingQuantity = item.Quantity;
                        existingBbd.Bbd = item.Bbd;
                    }
                    else
                    {
                        var newBbd = new TransactionItemBbd
                        {
                            TransactionItemsId = transactionItem.Id,
                            Quantity = item.Quantity,
                            Bbd = item.Bbd,
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            RemainingQuantity = item.Quantity,
                        };

                        await _context.TransactionItemBbd.AddAsync(newBbd, cancellationToken);
                    }
                }

                var providedIds = request.ItemBbds
                    .Where(x => x.BbdId.HasValue && x.BbdId.Value > 0)
                    .Select(x => x.BbdId.Value)
                    .ToList();

                foreach (var existing in originalBbdItems)
                {
                    if (!providedIds.Contains(existing.Id))
                    {
                        existing.IsActive = false;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                var totalQuantity = await _context.TransactionItemBbd
                    .Where(b => b.TransactionItemsId == transactionItem.Id && b.IsActive)
                    .SumAsync(b => b.Quantity, cancellationToken);
                transactionItem.RemainingQuantity = totalQuantity;

                var totalRemainingQuantity = await _context.TransactionItemBbd
                    .Where(b => b.TransactionItemsId == transactionItem.Id && b.IsActive)
                    .SumAsync(b => b.RemainingQuantity, cancellationToken);
                transactionItem.RemainingQuantity = totalRemainingQuantity;

                transactionItem.RemainingQuantity = totalRemainingQuantity;

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();

            }
        }
    }
}