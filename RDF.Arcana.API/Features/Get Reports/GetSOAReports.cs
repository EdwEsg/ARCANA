
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-soa-reports"), ApiController]
    public class GetSOAReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetSOAReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetSOAReportsCommand query)
        {
            try
            {
                var clienti = await _mediator.Send(query);

                if (clienti.IsFailure)
                {
                    return BadRequest(clienti);
                }
                return Ok(clienti);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetSOAReportsCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public DateTime? DateTo { get; set; }
        }

        public class GetSOAReportsResult
        {
            public string BusinessName { get; set; }
            public string FullName { get; set; }
            public ICollection<ClientSoa> PendingSoa { get; set; }
            public class ClientSoa
            {
                public DateTime DeliveryDate { get; set; }
                public string InvoiceNo { get; set; }
                public int Aging { get; set; }
                public decimal? Charges { get; set; }
                public decimal? TaxWithheld { get; set; }
                public decimal? Payment { get; set; }
                public decimal? RemainingBalance { get; set; }
                public string Remarks { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetSOAReportsCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetSOAReportsCommand request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo?.AddDays(1);

                var transactions = await _context.Transactions
                    .Where(t => t.ClientId == request.ClientId &&
                            (t.CreatedAt < adjustedDateTo || adjustedDateTo == null) &&
                             t.Status == Status.Pending)
                    .Select(t => new
                    {
                        CreatedDate = t.CreatedAt,
                        Invoice = t.InvoiceNo,
                        Status = t.Status,
                        Payment = t.TransactionSales.TotalAmountDue,
                        Balance = t.TransactionSales.RemainingBalance,
                        BusinessName = t.Client.BusinessName,
                        FullName = t.Client.Fullname
                    })
                    .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync(cancellationToken);


                if (!transactions.Any())
                {
                    var client = await _context.Clients
                        .Where(c => c.Id == request.ClientId)
                        .Select(c => new { c.BusinessName, c.Fullname })
                        .FirstOrDefaultAsync(cancellationToken);

                    var result = new GetSOAReportsResult
                    {
                        BusinessName = client?.BusinessName ?? "N/A",
                        FullName = client?.Fullname ?? "N/A",
                        PendingSoa = new List<GetSOAReportsResult.ClientSoa>()
                    };

                    return Result.Success(result);
                }


                var resultWithPending = new GetSOAReportsResult
                {
                    BusinessName = transactions.First().BusinessName,
                    FullName = transactions.First().FullName,
                    PendingSoa = transactions.Select(t => new GetSOAReportsResult.ClientSoa
                    {
                        DeliveryDate = t.CreatedDate,
                        InvoiceNo = t.Invoice,
                        Aging = (adjustedDateTo - t.CreatedDate)?.Days ?? 0,
                        Charges = t.Payment,
                        TaxWithheld = null,
                        Payment = t.Payment - t.Balance,
                        RemainingBalance = t.Balance,
                        Remarks = ""
                    }).ToList()
                };

                return Result.Success(resultWithPending);
            }
        }
    }
}
