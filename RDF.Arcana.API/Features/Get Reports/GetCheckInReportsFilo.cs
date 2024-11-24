
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-check-in-reports-filo"), ApiController]
    public class GetCheckInReportsFilo : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetCheckInReportsFilo(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCheckInFiloQuery query)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    query.AddedBy = userId;
                }

                var result = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    result.CurrentPage,
                    result.PageSize,
                    result.TotalCount,
                    result.TotalPages,
                    result.HasNextPage,
                    result.HasPreviousPage);

                var successResult = Result.Success(new
                {
                    result,
                    result.CurrentPage,
                    result.PageSize,
                    result.TotalCount,
                    result.TotalPages,
                    result.HasNextPage,
                    result.HasPreviousPage
                });

                return Ok(successResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public class GetCheckInFiloQuery : UserParams, IRequest<PagedList<GetCheckInFiloResult>>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class GetCheckInFiloResult
        {
            public string User { get; set; }
            public DateTime TimeIn { get; set; }
            public DateTime? TimeOut { get; set; }
            public string Duration { get; set; }
        }

        public class Handler : IRequestHandler<GetCheckInFiloQuery, PagedList<GetCheckInFiloResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }
            public async Task<PagedList<GetCheckInFiloResult>> Handle(GetCheckInFiloQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var query = _context.CheckIns
                    .Include(c => c.CreatedBy)
                        .ThenInclude(u => u.UserRoles)
                    .Include(c => c.CreatedBy)
                        .ThenInclude(u => u.CdoCluster)
                    .Where(t => t.CreatedDate >= request.DateFrom && t.CreatedDate < adjustedDateTo)
                    .AsQueryable();

                var userClusters = _context.CdoClusters
                    .FirstOrDefault(x => x.UserId == request.AddedBy);

                // Per CDO viewing
                if (userClusters != null)
                {
                    query = query.Where(c =>
                        c.CreatedBy.UserRoles.Id == 6 &&
                        c.CreatedBy.CdoCluster.ClusterId == userClusters.ClusterId);
                }

                if (request.ClusterId is not null)
                {
                    query = query.Where(c => c.CreatedBy.CdoCluster.ClusterId == request.ClusterId);
                }

                var resultQuery = query
                    .GroupBy(c => new
                    {
                        c.CreatedBy.Fullname,
                        Date = c.CreatedDate.Date
                    })
                    .Select(g => new GetCheckInFiloResult
                    {
                        User = g.Key.Fullname,
                        TimeIn = g.Min(x => x.CreatedDate),
                        TimeOut = g
                            .Where(x => x.TimeOut.HasValue)
                            .OrderByDescending(x => x.TimeOut)
                            .Select(x => x.TimeOut)
                            .FirstOrDefault(),
                        Duration = g.Where(x => x.TimeOut.HasValue).Any()
                            ? $"{(g.Where(x => x.TimeOut.HasValue).OrderByDescending(x => x.TimeOut).First().TimeOut.Value - g.Min(y => y.CreatedDate)).Days:D2}D " +
                              $"{(g.Where(x => x.TimeOut.HasValue).OrderByDescending(x => x.TimeOut).First().TimeOut.Value - g.Min(y => y.CreatedDate)).Hours:D2}H " +
                              $"{(g.Where(x => x.TimeOut.HasValue).OrderByDescending(x => x.TimeOut).First().TimeOut.Value - g.Min(y => y.CreatedDate)).Minutes:D2}M"
                            : null
                    })
                    .OrderByDescending(r => r.TimeIn);

                return await PagedList<GetCheckInFiloResult>.CreateAsync(resultQuery, request.PageNumber, request.PageSize);
            }
        }
    }
}
