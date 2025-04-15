using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-payment-reports"), ApiController]
    public class GetPaymentReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetPaymentReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPaymentReportsQuery query)
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

        public class GetPaymentReportsQuery : UserParams, IRequest<PagedList<GetPaymentReportResult>>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class GetPaymentReportResult
        {
            public DateTime Date { get; set; }
            public string TransactionId { get; set; }
            public string PaymentMethod { get; set; }
            public decimal PaymentAmount { get; set; }
            public string Payee { get; set; }
            public string AddedBy { get; set; }
            public string Reason { get; set; }
            public string Status { get; set; }
            public string ReferenceNo { get; set; }
        }

        public class Handler : IRequestHandler<GetPaymentReportsQuery, PagedList<GetPaymentReportResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetPaymentReportResult>> Handle(GetPaymentReportsQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var payments = _context.PaymentTransactions
                    .Include(x => x.AddedByUser)
                    .Include(t => t.Transaction)
                        .ThenInclude(c => c.Client)
                    .Where(ti =>
                        ti.DateReceived >= request.DateFrom &&
                        ti.DateReceived < adjustedDateTo &&
                        ti.Transaction.Status != Status.Voided)
                    .AsSplitQuery()
                    .AsNoTracking();

                if (request.ClusterId != null)
                {
                    payments = payments
                        .Where(p => p.Transaction.Client.ClusterId == request.ClusterId);
                }

                var result = payments.Select(p => new GetPaymentReportResult
                {
                    Date = p.DateReceived,
                    TransactionId = p.TransactionId.ToString(),
                    PaymentMethod = p.PaymentMethod,
                    PaymentAmount = p.PaymentAmount,
                    Payee = p.Payee,
                    AddedBy = p.AddedByUser.Fullname,
                    Reason = p.Reason,
                    Status = p.Status,
                    ReferenceNo = p.ReferenceNo,
                }).OrderBy(d => d.Date);

                return await PagedList<GetPaymentReportResult>
                    .CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
