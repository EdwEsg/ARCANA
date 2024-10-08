
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-sales-joural-reports"), ApiController]
    public class GetSalesJournalReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetSalesJournalReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetSalesJournalReportsQuery query)
        {
            try
            {
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
        public class GetSalesJournalReportsQuery : UserParams, IRequest<PagedList<GetSalesJournalReportsResult>>
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }

        public class GetSalesJournalReportsResult
        {
            public DateTime Date { get; set; }
            public string BusinessName { get; set; }
            public string BusinessAddress { get; set; }
            public string InvoiceNo { get; set; }
            public decimal Amount { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
        }

        public class Handler : IRequestHandler<GetSalesJournalReportsQuery, PagedList<GetSalesJournalReportsResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<PagedList<GetSalesJournalReportsResult>> Handle(GetSalesJournalReportsQuery request, CancellationToken cancellationToken)
            {
                var transactions = _context.Transactions
                    .Include(ts => ts.TransactionSales)
                    .Include(c => c.Client)
                        .ThenInclude(ba => ba.BusinessAddress)
                    .Where(t => t.CreatedAt >= request.DateFrom && t.CreatedAt <= request.DateTo)
                    .AsSplitQuery()
                .AsNoTracking();

                transactions = transactions.Where(t => t.CreatedAt >= request.DateFrom && t.CreatedAt <= request.DateTo);

                var result = transactions.Select(t => new GetSalesJournalReportsResult
                {
                    Date = t.CreatedAt,
                    BusinessName = t.Client.BusinessName,
                    BusinessAddress = $"{t.Client.BusinessAddress.City} {t.Client.BusinessAddress.Province}",
                    InvoiceNo = t.InvoiceNo,
                    Amount = t.TransactionSales.TotalAmountDue,
                    Debit = t.TransactionSales.TotalAmountDue - t.TransactionSales.RemainingBalance,
                    Credit = t.TransactionSales.RemainingBalance
                }).OrderBy(d => d.Date);

                return PagedList<GetSalesJournalReportsResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
