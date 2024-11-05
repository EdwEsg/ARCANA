
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.CheckIns
{
    [Route("api/get-clients-check-in"), ApiController]
    public class GetClientsForCheckIn : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetClientsForCheckIn(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetClientsForCheckInQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetClientsForCheckInQuery : IRequest<Result>
        {
            public string Origin { get; set; }
            public string Search { get; set; }
        }

        public class GetClientsForCheckInResult
        {
            public int ClientId { get; set; }
            public string FullName { get; set; }
            public string BusinessName { get; set; }
            public string StoreType { get; set; }
            public string Municipality { get; set; }
            public string BarangayName { get; set; }
        }

        public class Handler : IRequestHandler<GetClientsForCheckInQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetClientsForCheckInQuery request, CancellationToken cancellationToken)
            {
                var clients = await _context.Clients
                    .Include(st => st.StoreType)
                    .Include(b => b.BusinessAddress)
                    .Where(c => c.Origin == request.Origin && c.IsActive)
                    .ToListAsync(cancellationToken);


                if (!string.IsNullOrEmpty(request.Search))
                {
                    clients = clients.Where(c => c.BusinessName.Contains(request.Search) ||
                              c.Fullname.Contains(request.Search)).ToList();
                }

                var result = clients.Select(c => new GetClientsForCheckInResult
                {
                    ClientId = c.Id,
                    FullName = c.Fullname ?? "",
                    BusinessName = c.BusinessName ?? "",
                    StoreType = c.StoreType?.StoreTypeName ?? "",
                    Municipality = c.BusinessAddress?.City ?? "",
                    BarangayName = c.BusinessAddress?.Barangay ?? ""
                }).ToList();


                return Result.Success(result);

            }
        }
    }
}
