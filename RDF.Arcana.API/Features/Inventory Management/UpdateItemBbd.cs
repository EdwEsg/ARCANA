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
            public string ItemCode { get; set; }

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
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                // 1) If there's at least one brand-new BBD, create a TransactionBbd row 
                var skipCreation = request.Items.All(item =>
                    item.ItemBbds.All(b => b.BbdId.HasValue && b.BbdId.Value > 0)
                );

                TransactionBbd newTransactionBbd = null;
                if (!skipCreation)
                {
                    newTransactionBbd = new TransactionBbd
                    {
                        CreatedById = request.AccessBy,
                        ModifiedDate = DateTime.Now,
                        IsActive = true
                    };
                    await _context.TransactionBbd.AddAsync(newTransactionBbd, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    // If everything is an update, just bump ModifiedDate on *some* existing row
                    var existingItemBbd = await _context.TransactionItemBbd
                        .Include(tib => tib.TransactionBbd)
                        .Where(tib => tib.IsActive)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (existingItemBbd?.TransactionBbd != null)
                    {
                        existingItemBbd.TransactionBbd.ModifiedDate = DateTime.Now;
                        existingItemBbd.CreatedDate = DateTime.Now;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }

                // 2) Process each item code
                foreach (var itemDto in request.Items)
                {
                    // A) Gather all existing BBD rows for that item code
                    var existingBbdRows = await _context.TransactionItemBbd
                        .Where(x =>
                            x.ItemCode == itemDto.ItemCode
                            && x.IsActive
                        )
                        .ToListAsync(cancellationToken);

                    // B) Deactivate any existing rows not in the request
                    var bbdIdsInRequest = itemDto.ItemBbds
                        .Where(b => b.BbdId.HasValue && b.BbdId.Value > 0)
                        .Select(b => b.BbdId.Value)
                        .ToHashSet();

                    foreach (var dbBbd in existingBbdRows)
                    {
                        if (!bbdIdsInRequest.Contains(dbBbd.Id))
                        {
                            dbBbd.IsActive = false;
                        }
                    }
                    await _context.SaveChangesAsync(cancellationToken);

                    // C) Update or create each BBD from the request
                    foreach (var bbdInput in itemDto.ItemBbds)
                    {
                        if (bbdInput.BbdId.HasValue && bbdInput.BbdId.Value > 0)
                        {
                            // Update existing
                            var found = existingBbdRows
                                .FirstOrDefault(x => x.Id == bbdInput.BbdId.Value);

                            if (found == null)
                            {
                                return InventoryErrors.TransactionItemNotFound(
                                    bbdInput.BbdId.Value.ToString(),
                                    user?.Fullname
                                );
                            }

                            if (bbdInput.Quantity < 0)
                            {
                                return InventoryErrors.InvalidRemainingInventory(
                                    bbdInput.Quantity,
                                    found.Quantity,
                                    found.Id.ToString()
                                );
                            }

                            // Overwrite quantity & Bbd
                            found.Quantity = bbdInput.Quantity;
                            found.RemainingQuantity = bbdInput.Quantity;
                            found.Bbd = bbdInput.Bbd;
                        }
                        else
                        {
                            // brand-new BBD => create
                            if (bbdInput.Quantity < 0)
                            {
                                return InventoryErrors.InvalidRemainingInventory(
                                    0,
                                    bbdInput.Quantity,
                                    itemDto.ItemCode
                                );
                            }

                            var newBbd = new TransactionItemBbd
                            {
                                Quantity = bbdInput.Quantity,
                                RemainingQuantity = bbdInput.Quantity,
                                Bbd = bbdInput.Bbd,
                                IsActive = true,
                                CreatedDate = DateTime.Now,
                                TransactionBbdId = newTransactionBbd?.Id ?? 0,
                                ItemCode = itemDto.ItemCode
                            };
                            await _context.TransactionItemBbd.AddAsync(newBbd, cancellationToken);
                        }
                    }
                    await _context.SaveChangesAsync(cancellationToken);

                    // D) Now re-compute T's RemQty = sum of *all active* BBD rows for that item code
                    decimal sumAllActiveBbd = await _context.TransactionItemBbd
                        .Where(b => b.ItemCode == itemDto.ItemCode && b.IsActive)
                        .SumAsync(b => b.Quantity, cancellationToken);

                    // E) Update *all* T's for that code => set .RemainingQuantity = sumAllActiveBbd
                    var allTforCode = await _context.TransactionItems
                        .Where(ti =>
                            ti.Item.ItemCode == itemDto.ItemCode
                            && ti.Transaction.AddedBy == request.AccessBy
                            && ti.IsActive
                        )
                        .ToListAsync(cancellationToken);

                    foreach (var tRow in allTforCode)
                    {
                        tRow.RemainingQuantity = sumAllActiveBbd;
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Result.Success();
            }
        }
    }
}