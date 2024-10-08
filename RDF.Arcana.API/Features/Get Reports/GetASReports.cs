using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-as-reports"), ApiController]
    public class GetASReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetASReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetASReportsQuery query)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
               && IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    query.AddedBy = userId;

                    var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                }

                var transactions = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage);
                var result = new
                {
                    transactions,
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetASReportsQuery : UserParams, IRequest<PagedList<GetAsReportsResult>>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }

        public class GetAsReportsResult
        {
            public DateTime Date { get; set; }
            public string BusinessName { get; set; }
            public string CustomerName { get; set; }
            public string InvoiceNo { get; set; }
            public decimal GrossSales { get; set; }
            public decimal NetSales { get; set; }
            public string Cluster { get; set; }
        }

        public class Handler : IRequestHandler<GetASReportsQuery, PagedList<GetAsReportsResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetAsReportsResult>> Handle(GetASReportsQuery request, CancellationToken cancellationToken)
            {
                var transactions = _context.Transactions
                    .Include(c => c.Client)
                        .ThenInclude(cl => cl.Cluster)
                            .ThenInclude(cdo => cdo.CdoClusters)
                    .Include(ts => ts.TransactionSales)
                    .AsSplitQuery()
                    .AsNoTracking();

                var currentUserRole = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == request.AddedBy, cancellationToken);

                //acess only for admin and finance 
                if (request.AddedBy == 1 || currentUserRole.UserRolesId == 9)
                {
                    transactions = transactions.Where(t => t.CreatedAt >= request.DateFrom && t.CreatedAt <= request.DateTo);
                }
                else
                {
                    throw new UnauthorizedAccessException("Unauthorized");
                }

                var result = transactions.Select(t => new GetAsReportsResult
                {
                    Date = t.CreatedAt,
                    BusinessName = t.Client.BusinessName,
                    CustomerName = t.Client.Fullname,
                    InvoiceNo = t.InvoiceNo,
                    GrossSales = t.TransactionSales.SubTotal,
                    NetSales = t.TransactionSales.TotalAmountDue,
                    Cluster = t.Client.Cluster.ClusterType

                }).OrderBy(d => d.Date);

                return await PagedList<GetAsReportsResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
