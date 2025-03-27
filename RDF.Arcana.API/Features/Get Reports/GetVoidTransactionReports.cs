using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-void-transaction-reports"), ApiController]
    public class GetVoidTransactionReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetVoidTransactionReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetVoidTransactionReportsQuery query)
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

        public class GetVoidTransactionReportsQuery : UserParams, IRequest<PagedList<GetVoidTransactionReportsResult>>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class GetVoidTransactionReportsResult
        {
            public DateTime Date { get; set; }
            public string InvoiceNo { get; set; }
            public string ItemDescription { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Amount { get; set; }
            public string Outlet { get; set; }
            public string Status { get; set; }
        }

        public class Handler : IRequestHandler<GetVoidTransactionReportsQuery, PagedList<GetVoidTransactionReportsResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetVoidTransactionReportsResult>> Handle(GetVoidTransactionReportsQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var transactionItems = _context.TransactionItems
                    .Include(t => t.Transaction)
                        .ThenInclude(ts => ts.TransactionSales)
                    .Include(t => t.Transaction)
                        .ThenInclude(c => c.Client)
                    .Include(i => i.Item)
                    .Where(ti =>
                        ti.CreatedAt >= request.DateFrom &&
                        ti.CreatedAt < adjustedDateTo &&
                        ti.Transaction.Status == Status.Voided
                    )
                    .AsSplitQuery()
                    .AsNoTracking();

                if (request.ClusterId is not null)
                {
                    transactionItems = transactionItems
                        .Where(ti => ti.Transaction.Client.ClusterId == request.ClusterId);
                }

                if (request.AddedBy is int userId)
                {
                    if (userId != 1)
                    {
                        bool userHasAnyItems = await _context.TransactionItems
                            .AnyAsync(
                                ti => ti.AddedBy == userId &&
                                      ti.CreatedAt >= request.DateFrom &&
                                      ti.CreatedAt < adjustedDateTo,
                                cancellationToken
                            );

                        if (userHasAnyItems)
                        {
                            transactionItems = transactionItems
                                .Where(ti => ti.AddedBy == userId);
                        }
                    }
                }

                var resultQuery = transactionItems.Select(t => new GetVoidTransactionReportsResult
                {
                    Date = t.CreatedAt,
                    InvoiceNo = t.Transaction.InvoiceNo,
                    ItemDescription = t.Item.ItemDescription,
                    Quantity = t.Quantity,
                    UnitPrice = t.UnitPrice,
                    Amount = t.Amount,
                    Outlet = t.Transaction.Client.BusinessName,
                    Status = t.Transaction.Status,
                })
                .OrderBy(d => d.Date);

                return await PagedList<GetVoidTransactionReportsResult>
                    .CreateAsync(resultQuery, request.PageNumber, request.PageSize);
            }
        }
    }
}
