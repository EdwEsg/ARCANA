using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-cdo-reports"), ApiController]
    public class GetCDOReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetCDOReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCDOReportsQuery query)
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

        public class GetCDOReportsQuery : UserParams, IRequest<PagedList<GetCDOReportsResult>>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }

        public class GetCDOReportsResult
        {
            public DateTime Date { get; set; }
            public string InvoiceNo { get; set; }
            public string ItemDescription { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Amount { get; set; }
            public string Outlet { get; set; }
        }

        public class Handler : IRequestHandler<GetCDOReportsQuery, PagedList<GetCDOReportsResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetCDOReportsResult>> Handle(GetCDOReportsQuery request, CancellationToken cancellationToken)
            {
                var transactionItem = _context.TransactionItems
                    .Include(t => t.Transaction)
                        .ThenInclude(ts => ts.TransactionSales)
                    .Include(t => t.Transaction)
                        .ThenInclude(c => c.Client)
                    .Include(i => i.Item)
                    .AsSplitQuery()
                    .AsNoTracking();

                if (request.AddedBy == 1)
                {
                    transactionItem = transactionItem.Where(ti => ti.CreatedAt >= request.DateFrom && ti.CreatedAt <= request.DateTo);
                }
                else
                {
                    transactionItem = transactionItem.Where(ti => ti.CreatedAt >= request.DateFrom && ti.CreatedAt <= request.DateTo
                                              && ti.AddedBy == request.AddedBy);

                    var hasMatchingItems = await transactionItem.AnyAsync(cancellationToken);
                    if (!hasMatchingItems)
                    {
                        throw new UnauthorizedAccessException("Unauthorized");
                    }
                }

                var result = transactionItem.Select(t => new GetCDOReportsResult
                {
                    Date = t.CreatedAt,
                    InvoiceNo = t.Transaction.InvoiceNo,
                    ItemDescription = t.Item.ItemDescription,
                    Quantity = t.Quantity,
                    UnitPrice = t.UnitPrice,
                    Amount = t.Amount,
                    Outlet = t.Transaction.Client.BusinessName
                }).OrderBy(d => d.Date);

                return await PagedList<GetCDOReportsResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
