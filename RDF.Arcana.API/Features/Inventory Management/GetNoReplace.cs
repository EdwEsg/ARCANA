using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-no-replace"), ApiController]
    public class GetNoReplace : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetNoReplace(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var query = new GetNoReplaceQuery
                {
                    ClientId = id
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetNoReplaceQuery : IRequest<Result>
        {
            public int ClientId { get; set; }
        }

        public class GetNoReplaceResult
        {
            public string BusinessName { get; set; }
            public decimal TotalRemaining { get; set; }
            public IEnumerable<SalesReturnDto> SalesReturn { get; set; }
            public class SalesReturnDto
            {
                public DateTime CreatedAt { get; set; }
                public decimal Remaining { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetNoReplaceQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetNoReplaceQuery request, CancellationToken cancellationToken)
            {
                var salesReturn = await _context.SalesReturns
                    .Include(c => c.Client)
                    .Where(sr => sr.ClientId == request.ClientId && sr.IsActive)
                    .ToListAsync(cancellationToken);

                var salesReturnResult = salesReturn.Select(sr => new GetNoReplaceResult.SalesReturnDto
                {
                    CreatedAt = sr.CreatedDate,
                    Remaining = sr.RemainingBalance
                }).ToList();

                var result = new GetNoReplaceResult
                {
                    BusinessName = salesReturn.FirstOrDefault()?.Client.BusinessName ?? string.Empty,
                    TotalRemaining = salesReturnResult.Sum(s => s.Remaining),
                    SalesReturn = salesReturnResult
                };

                return Result.Success(result);
            }
        }
    }
}
