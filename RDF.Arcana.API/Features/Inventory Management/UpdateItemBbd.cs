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
            public int? ClientId { get; set; }

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

                foreach (var itemDto in request.Items)
                {
                    var existingBbdRows = await _context.TransactionItemBbd
                        .Where(x =>
                            x.ItemCode == itemDto.ItemCode
                            && x.IsActive
                        )
                        .ToListAsync(cancellationToken);

                    var bbdIdsInRequest = itemDto.ItemBbds
                        .Where(b => b.BbdId.HasValue && b.BbdId.Value > 0)
                        .Select(b => b.BbdId.Value)
                        .ToHashSet();

                    foreach (var dbBbd in existingBbdRows)
                    {
                        if (!bbdIdsInRequest.Contains(dbBbd.Id))
                        {
                            dbBbd.IsActive = true;
                        }
                    }
                    await _context.SaveChangesAsync(cancellationToken);

                    foreach (var bbdInput in itemDto.ItemBbds)
                    {
                        if (bbdInput.BbdId.HasValue && bbdInput.BbdId.Value > 0)
                        {
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

                            //found.Quantity = bbdInput.Quantity;
                            found.RemainingQuantity = bbdInput.Quantity;
                            found.Bbd = bbdInput.Bbd;
                        }
                        else
                        {
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
                                ClientsId = request.ClientId,
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

                    decimal sumAllActiveBbd = await _context.TransactionItemBbd
                        .Where(b => b.ItemCode == itemDto.ItemCode && b.IsActive)
                        .SumAsync(b => b.RemainingQuantity, cancellationToken);

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