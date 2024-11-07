
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;
using static RDF.Arcana.API.Features.CheckIns.GetCheckIn;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-check-in-reports"), ApiController]
    public class GetCheckInReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetCheckInReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCheckInReportsQuery query)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
               && IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    query.AddedBy = userId;

                    var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                }

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

        public class GetCheckInReportsQuery : UserParams, IRequest<PagedList<GetCheckInReportsResult>>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class GetCheckInReportsResult
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

        public class Handler : IRequestHandler<GetCheckInReportsQuery, PagedList<GetCheckInReportsResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetCheckInReportsResult>> Handle(GetCheckInReportsQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var checkIn = _context.CheckIns
                    .Include(c => c.Client)
                        .ThenInclude(b => b.BusinessAddress)
                    .Include(c => c.CreatedBy)
                        .ThenInclude(u => u.UserRoles)
                    .Include(c => c.CreatedBy)
                        .ThenInclude(u => u.CdoCluster)
                    .Where(t => t.CreatedDate >= request.DateFrom && t.CreatedDate < adjustedDateTo)
                    .AsQueryable();

                var userClusters = _context.CdoClusters
                    .FirstOrDefault(x => x.UserId == request.AddedBy);

                //per CDO viewing
                if (userClusters != null)
                {
                    checkIn = checkIn.Where(c =>
                        c.CreatedBy.UserRoles.Id == 6 &&
                        c.CreatedBy.CdoCluster.ClusterId == userClusters.ClusterId);
                }

                //filter for Admin/Finanace/GAS/Treasury
                var adminClusterFilter = _context.Users.Find(request.AddedBy);
                if ((adminClusterFilter.UserRolesId == 1 ||
                    adminClusterFilter.UserRolesId == 7 ||
                    adminClusterFilter.UserRolesId == 8 ||
                    adminClusterFilter.UserRolesId == 9 ||
                    adminClusterFilter.UserRolesId == 10)
                    && request.ClusterId is not null)
                {
                    checkIn = checkIn.Where(c => c.CreatedBy.CdoCluster.ClusterId == request.ClusterId);
                }

                var result = checkIn
                            .OrderByDescending(ck => ck.CreatedDate)
                            .Select(ck => new GetCheckInReportsResult
                            {
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
                                CreatedDate = ck.CreatedDate
                            });

                return await PagedList<GetCheckInReportsResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
