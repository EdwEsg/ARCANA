
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.CheckIns
{
    [Route("api/get-check-in"), ApiController]
    public class GetCheckIn : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetCheckIn(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCheckInQuery query)
        {
            try
            {              
                var checkIn = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    checkIn.CurrentPage,
                    checkIn.PageSize,
                    checkIn.TotalCount,
                    checkIn.TotalPages,
                    checkIn.HasNextPage,
                    checkIn.HasPreviousPage);
                var result = new
                {
                    checkIn,
                    checkIn.CurrentPage,
                    checkIn.PageSize,
                    checkIn.TotalCount,
                    checkIn.TotalPages,
                    checkIn.HasNextPage,
                    checkIn.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetCheckInQuery : UserParams, IRequest<PagedList<GetCheckInResult>>
        {
            public string Search { get; set; }
        }

        public class GetCheckInResult
        {
            public string BusinessName { get; set; }
            public string FullName { get; set; }
            public string HouseNo { get; set; }
            public string StreetName { get; set; }
            public string Barangay { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Image { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class Handler : IRequestHandler<GetCheckInQuery, PagedList<GetCheckInResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetCheckInResult>> Handle(GetCheckInQuery request, CancellationToken cancellationToken)
            {
                var checkIn = _context.CheckIns
                    .Include(c => c.Client)
                        .ThenInclude(b => b.BusinessAddress)
                    .Include(u => u.CreatedBy)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Search))
                {
                    checkIn = checkIn.Where(ck => ck.Client.Fullname.Contains(request.Search) ||
                              ck.Client.BusinessName.Contains(request.Search));
                }

                var result = checkIn.Select(ck => new GetCheckInResult
                {
                    BusinessName = ck.Client.BusinessName,
                    FullName = ck.Client.Fullname,
                    HouseNo = ck.Client.BusinessAddress.HouseNumber,
                    StreetName = ck.Client.BusinessAddress.StreetName,
                    Barangay = ck.Client.BusinessAddress.Barangay,
                    City = ck.Client.BusinessAddress.City,
                    Province = ck.Client.BusinessAddress.Province,
                    Latitude = ck.Latitude,
                    Longitude = ck.Longitude,
                    Image = ck.Image,
                    Remarks = ck.Remarks,
                    CreatedBy = ck.CreatedBy.Fullname,
                    CreatedDate = ck.CreatedDate

                }).OrderByDescending(d => d.CreatedDate);

                return await PagedList<GetCheckInResult>.CreateAsync(result, request.PageNumber, request.PageSize);
                

            }
        }
    }
}
