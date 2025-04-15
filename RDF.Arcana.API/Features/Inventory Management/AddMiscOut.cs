using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-misc-out"), ApiController]
    public class AddMiscOut : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddMiscOut(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddMiscOutCommand command)
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

        public class AddMiscOutCommand : IRequest<Result>
        {
            public int AccessBy { get; set; }
            public int? ExternalMoveOrderId { get; set; }
            public int ToCdo { get; set; }
            public List<MiscOutItemsDto> MiscOutItems { get; set; }
            public class MiscOutItemsDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public string Bbd { get; set; }
                public string Reason { get; set; }
            }
        }

        public class Handler : IRequestHandler<AddMiscOutCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddMiscOutCommand request, CancellationToken cancellationToken)
            {

                var miscOut = new MiscellaneousOut
                {
                    CreatedById = request.AccessBy,
                    ToCdo = request.ToCdo,
                    ExternalMoveOrderId = request.ExternalMoveOrderId,
                };

                await _context.MiscellaneousOuts.AddAsync(miscOut, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var moveOrderItems = await _context.MoveOrderItems
                    .Include(m => m.MoveOrder)
                    .Where(m => m.CreatedBy.Id == request.ToCdo &&
                        m.IsActive &&
                        m.RemainingQuantity > 0)
                    .OrderBy(m => m.MoveOrder.CreatedDate)
                    .ToListAsync(cancellationToken);

                var miscOutItemsList = new List<MiscellaneousOutItems>();

                foreach (var reqItem in request.MiscOutItems)
                {
                    var neededQty = reqItem.Quantity;
                    if (neededQty <= 0) continue;

                    var matchingMoItems = moveOrderItems
                        .Where(x => x.ItemId == reqItem.ItemId && x.RemainingQuantity > 0)
                        .ToList();

                    foreach (var moItem in matchingMoItems)
                    {
                        if (neededQty <= 0) break;

                        var availableQty = moItem.RemainingQuantity ?? 0;
                        if (availableQty >= neededQty)
                        {
                            moItem.RemainingQuantity -= neededQty;
                            neededQty = 0;
                        }
                        else
                        {
                            neededQty -= availableQty;
                            moItem.RemainingQuantity = 0;
                        }
                    }

                    var miscOutItem = new MiscellaneousOutItems
                    {
                        MiscellaneousOutId = miscOut.Id,
                        ItemId = reqItem.ItemId,
                        Quantity = reqItem.Quantity,
                        Bbd = reqItem.Bbd,
                        Reason = reqItem.Reason,
                    };

                    miscOutItemsList.Add(miscOutItem);
                }
                _context.MiscellaneousOutItems.AddRange(miscOutItemsList);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
