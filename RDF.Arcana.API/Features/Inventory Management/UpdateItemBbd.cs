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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMultipleBbdCommand command)
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

        public class UpdateMultipleBbdCommand : IRequest<Result>
        {
            public int AccessBy { get; set; }

            public List<UpdateItemBbdDto> Items { get; set; } = new();
        }

        public class UpdateItemBbdDto
        {
            public int TransactionItemId { get; set; }

            public List<ItemBbdDto> ItemBbds { get; set; } = new();
        }

        public class ItemBbdDto
        {
            public int? BbdId { get; set; }
            public decimal Quantity { get; set; }
            public DateTime Bbd { get; set; }
        }


        public class BbdHandler : IRequestHandler<UpdateMultipleBbdCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public BbdHandler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateMultipleBbdCommand request, CancellationToken cancellationToken)
            {
                var currentUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                foreach (var itemDto in request.Items)
                {
                    var transactionItem = await _context.TransactionItems
                        .Include(ti => ti.Transaction)
                        .Include(ti => ti.TransactionItemBbd)
                        .FirstOrDefaultAsync(
                            ti => ti.Id == itemDto.TransactionItemId
                                  && ti.Transaction.AddedBy == request.AccessBy,
                            cancellationToken);

                    if (transactionItem == null)
                    {

                        return InventoryErrors.TransactionItemNotFound(
                            itemDto.TransactionItemId.ToString(),
                            currentUser?.Fullname
                        );
                    }

                    if (!itemDto.ItemBbds.Any())
                    {
                        foreach (var existingBbd in transactionItem.TransactionItemBbd.Where(x => x.IsActive))
                        {
                            existingBbd.IsActive = false;
                        }

                        transactionItem.RemainingQuantity = transactionItem.Quantity;
                        await _context.SaveChangesAsync(cancellationToken);
                        continue;
                    }

                    decimal sumOfIncomingQuantities = itemDto.ItemBbds.Sum(x => x.Quantity);

                    if (sumOfIncomingQuantities > transactionItem.Quantity || sumOfIncomingQuantities < 0)
                    {
                        return InventoryErrors.InvalidRemainingInventory(
                            transactionItem.Quantity,
                            sumOfIncomingQuantities,
                            transactionItem.Id.ToString()
                        );
                    }

                    var originalBbdItems = transactionItem.TransactionItemBbd
                        .Where(x => x.IsActive)
                        .ToList();

                    foreach (var bbdInput in itemDto.ItemBbds)
                    {
                        if (bbdInput.BbdId.HasValue && bbdInput.BbdId.Value > 0)
                        {
                            var existingBbd = originalBbdItems
                                .FirstOrDefault(b => b.Id == bbdInput.BbdId.Value && b.IsActive);

                            if (existingBbd == null)
                            {
                                return InventoryErrors.TransactionItemNotFound(
                                    bbdInput.BbdId.Value.ToString(),
                                    currentUser?.Fullname
                                );
                            }

                            if (existingBbd.Quantity < bbdInput.Quantity)
                            {
                                return InventoryErrors.InvalidRemainingInventory(
                                    bbdInput.Quantity,
                                    existingBbd.Quantity,
                                    existingBbd.Id.ToString()
                                );
                            }

                            existingBbd.RemainingQuantity = bbdInput.Quantity;
                            existingBbd.Bbd = bbdInput.Bbd;
                        }
                        else
                        {
                            var newBbd = new TransactionItemBbd
                            {
                                TransactionItemsId = transactionItem.Id,
                                Quantity = bbdInput.Quantity,
                                Bbd = bbdInput.Bbd,
                                IsActive = true,
                                CreatedDate = DateTime.Now,
                                RemainingQuantity = bbdInput.Quantity,
                            };

                            await _context.TransactionItemBbd.AddAsync(newBbd, cancellationToken);
                        }
                    }

                    var providedIds = itemDto.ItemBbds
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

                    var totalRemainingQuantity = await _context.TransactionItemBbd
                        .Where(b => b.TransactionItemsId == transactionItem.Id && b.IsActive)
                        .SumAsync(b => b.RemainingQuantity, cancellationToken);

                    transactionItem.RemainingQuantity = totalRemainingQuantity;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Result.Success();
            }
        }
    }
}