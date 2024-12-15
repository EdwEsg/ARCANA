using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.AddNewPaymentTransaction.AddNewPaymentTransactionCommand;

namespace Arcana.API.Features.Sales_Management.Clearing.Transaction
{
    [Route("api/sandbox")]
    [ApiController]
    public class GetAllRemittance : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllRemittance(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllRemittanceQuery query)
        {
            try
            {
                var transactions = await _mediator.Send(query);

                Response.AddPaginationHeader(transactions.CurrentPage, transactions.PageSize, transactions.TotalCount,
                    transactions.TotalPages, transactions.HasNextPage, transactions.HasPreviousPage);

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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class GetAllRemittanceQuery : UserParams, IRequest<PagedList<GetAllRemittanceResult>>
    {
        public int TransactionId { get; set; }
    }

    public class GetAllRemittanceResult
    {
        public string BusinessName { get; set; }
        public string Status { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }
        public IEnumerable<PaymentTran> Payments { get; set; }
        public class PaymentTran
        {
            public decimal Payment { get; set; }
            public string PaymentMethod { get; set; }
        }
        public IEnumerable<ClearedPaymentDto> Cleared { get; set; }
        public class ClearedPaymentDto
        {
            public string Status { get; set; }
            public string Atag { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllRemittanceQuery, PagedList<GetAllRemittanceResult>>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

public async Task<PagedList<GetAllRemittanceResult>> Handle(GetAllRemittanceQuery request, CancellationToken cancellationToken)
{
    var query2 = _context.Transactions
        .AsNoTracking()
        .Where(t => t.Status == Status.Paid)
        .Select(t => new GetAllRemittanceResult
        {
            BusinessName = t.Client.BusinessName,
            Status = t.Status,
            InvoiceNo = t.InvoiceNo,
            InvoiceType = t.InvoiceType,
            Payments = t.PaymentTransactions.Select(pt => new GetAllRemittanceResult.PaymentTran
            {
                Payment = pt.TotalAmountReceived,
                PaymentMethod = pt.PaymentMethod,
            }),
            Cleared = t.PaymentTransactions
                .Where(pt => pt.ClearedPayment != null)
                .Select(pt => new GetAllRemittanceResult.ClearedPaymentDto
                {
                    Status = pt.ClearedPayment.Status,
                    Atag = pt.ClearedPayment.ATag,
                })
        });

    return await PagedList<GetAllRemittanceResult>.CreateAsync(query2, request.PageNumber, request.PageSize);
}

    }
}
