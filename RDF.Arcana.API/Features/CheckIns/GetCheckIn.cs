
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }

        public class GetCheckInResult
        {
            public int Id { get; set; }
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
            public DateTime? TimeOut { get; set; }
            public string Duration { get; set; }
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
                var adjustedDateTo = request.DateTo.AddDays(1);

                var checkIn = _context.CheckIns
                    .Include(c => c.Client)
                        .ThenInclude(b => b.BusinessAddress)
                    .Include(u => u.CreatedBy)
                    .Where(c =>
                        c.CreatedDate >= request.DateFrom && c.CreatedDate < adjustedDateTo)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Search))
                {
                    checkIn = checkIn.Where(ck =>
                        (ck.Client != null && (ck.Client.Fullname.Contains(request.Search) || ck.Client.BusinessName.Contains(request.Search))) ||
                        (ck.ClientId == null && (ck.BusinessNameOthers.Contains(request.Search) || ck.FullNameOthers.Contains(request.Search)))
                    );
                }

                
                var result = checkIn
                            .OrderByDescending(ck => ck.CreatedDate)
                            .Select(ck => new GetCheckInResult
                            {
                                Id = ck.Id,
                                BusinessName = ck.ClientId == null ? ck.BusinessNameOthers : ck.Client.BusinessName,
                                FullName = ck.ClientId == null ? ck.FullNameOthers : ck.Client.Fullname,
                                HouseNo = ck.ClientId == null ? null : ck.Client.BusinessAddress.HouseNumber,
                                StreetName = ck.ClientId == null ? null : ck.Client.BusinessAddress.StreetName,
                                Barangay = ck.ClientId == null ? ck.BarangayOthers : ck.Client.BusinessAddress.Barangay,
                                City = ck.ClientId == null ? ck.CityOthers : ck.Client.BusinessAddress.City,
                                Province = ck.ClientId == null ? ck.ProvinceOthers : ck.Client.BusinessAddress.Province,
                                Latitude = ck.Latitude,
                                Longitude = ck.Longitude,
                                Image = ck.Image,
                                Remarks = ck.Remarks,
                                CreatedBy = ck.CreatedBy.Fullname,
                                CreatedDate = ck.CreatedDate,
                                TimeOut = ck.TimeOut,
                                Duration = ck.TimeOut.HasValue
                                    ? $"{(ck.TimeOut.Value - ck.CreatedDate).Days:D2}D {(ck.TimeOut.Value - ck.CreatedDate).Hours:D2}H {(ck.TimeOut.Value - ck.CreatedDate).Minutes:D2}M"
                                    : null,

                            });


                return await PagedList<GetCheckInResult>.CreateAsync(result, request.PageNumber, request.PageSize);
                

            }
        }
    }
}
