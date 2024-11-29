
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Models.ExternalDb;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/try-get-inventory"), ApiController]
    public class TryGetInventory : ControllerBase
    {
        private readonly IMediator _mediator;
        public TryGetInventory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TryGetInventoryRequest query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class TryGetInventoryRequest : IRequest<Result>
        {
            public int MoveOrderId { get; set; }
        }

        public class TryGetInventoryResult
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<TryGetInventoryRequest, Result>
        {
            private readonly ExternalDbContext _exteral;

            public Handler(ExternalDbContext exteral)
            {
                _exteral = exteral;
            }

            public async Task<Result> Handle(TryGetInventoryRequest request, CancellationToken cancellationToken)
            {
                var moveOrder = await _exteral.MoveOrders.FirstOrDefaultAsync(mo => mo.Id == request.MoveOrderId, cancellationToken);

                var result = new TryGetInventoryResult
                {
                    Id = moveOrder.Id,
                    Description = moveOrder.Description,
                };

                return Result.Success(result);
            }
        }
    }
}
