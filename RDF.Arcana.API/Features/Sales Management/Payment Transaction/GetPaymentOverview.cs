using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;


namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction
{
	[Route("api/payment"), ApiController]
	public class GetPaymentOverview : ControllerBase
	{
		private readonly IMediator _mediator;

		public GetPaymentOverview(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("overview")]
		public async Task<IActionResult> GetPaymentOverviewAsync([FromQuery] GetPaymentOverviewRequest query)
		{
            if (User.Identity is ClaimsIdentity identity
               && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                query.AddedBy = userId;
            }

            var result = await _mediator.Send(query);

			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		public class GetPaymentOverviewRequest : IRequest<Result>
		{
			public int PaymentRecordId { get; set; }
            public string PaymentMethod { get; set; }
            public string CheckReferenceNo { get; set; }
            public int AddedBy { get; set; }
        }

		public class GetPaymentOverviewResponse
		{
            public string PaymentMethod { get; set; }
            public string PaymentChannel { get; set; }
            public string CreatedAt { get; set; }
            public string AddedBy { get; set; }
            public decimal TotalAmount { get; set; }
			public string ModeOfPayment { get; set; }
			public ICollection<Transaction> Transactions { get; set; }
			public decimal? AdvancePaymentAmount { get; set; }
            public string Reason { get; set; }
            public string ReceiptNo { get; set; }
            public string ReceiptAttachment { get; set; }
            public string Attachment { get; set; }
            public string WithholdingNo { get; set; }

            public class Transaction
			{
                public int PaymentTransactionId { get; set; }
                public string InvoiceType { get; set; }
                public string InvoiceNo { get; set; }
                public decimal TotalAmount { get; set; }
                public decimal PaymentAmount { get; set; }
                public ICollection<TransactionItem> TransactionItems { get; set; }

            }

			public class TransactionItem
			{
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public decimal Quantity { get; set; }
                public decimal Amount { get; set; }
            }
			
        }

		public class Handler : IRequestHandler<GetPaymentOverviewRequest, Result>
		{
			private readonly ArcanaDbContext _context;

			public Handler(ArcanaDbContext context)
			{
				_context = context;
			}

            public async Task<Result> Handle(GetPaymentOverviewRequest request, CancellationToken cancellationToken)
            {
                var query = _context.PaymentTransactions
                    .Where(pt => pt.PaymentRecordId == request.PaymentRecordId &&
                                 pt.PaymentMethod == request.PaymentMethod)
                    .Select(pt => new
                    {
                        pt,
                        pt.PaymentRecord,
                        pt.PaymentRecord.ReceiptNo,
                        pt.PaymentRecord.Receipt,
                        AddedByUserFullName = pt.AddedByUser.Fullname,
                        TransactionItems = pt.Transaction.TransactionItems.Select(ti => new
                        {
                            ti.Item.ItemCode,
                            ti.Item.ItemDescription,
                            ti.Quantity,
                            ti.Amount
                        }),
                        pt.Transaction.InvoiceType,
                        pt.Transaction.InvoiceNo,
                        AdvancePaymentAmount = _context.AdvancePayments
                            .Where(ap => ap.ChequeNo == request.CheckReferenceNo)
                            .Select(ap => (decimal?)ap.AdvancePaymentAmount)
                            .FirstOrDefault()
                    });

                var paymentOverview = await query
                    .GroupBy(x => new
                    {
                        x.pt.PaymentMethod,
                        x.pt.BankName,
                        x.pt.ChequeDate,
                        x.AddedByUserFullName,
                        x.pt.Reason,
                        x.ReceiptNo,
                        x.Receipt,
                        x.AdvancePaymentAmount,
                        x.pt.WithholdingAttachment,
                        x.pt.WithholdingNo
                    })
                    .Select(g => new GetPaymentOverviewResponse
                    {
                        PaymentMethod = g.Key.PaymentMethod,
                        PaymentChannel = g.Key.BankName,
                        CreatedAt = g.Key.ChequeDate.ToString("MM-dd-yyyy"),
                        AddedBy = g.Key.AddedByUserFullName,
                        TotalAmount = g.Sum(x => x.pt.TotalAmountReceived),
                        ModeOfPayment = g.Key.PaymentMethod,
                        Reason = g.Key.Reason,
                        ReceiptNo = g.Key.ReceiptNo,
                        ReceiptAttachment = g.Key.Receipt,
                        Attachment = g.Key.WithholdingAttachment,
                        WithholdingNo = g.Key.WithholdingNo,
                        Transactions = g.Select(x => new GetPaymentOverviewResponse.Transaction
                        {
                            PaymentTransactionId = x.pt.TransactionId,
                            InvoiceType = x.InvoiceType,
                            InvoiceNo = x.InvoiceNo,
                            PaymentAmount = x.pt.TotalAmountReceived,
                            TransactionItems = x.TransactionItems.Select(ti => new GetPaymentOverviewResponse.TransactionItem
                            {
                                ItemCode = ti.ItemCode,
                                ItemDescription = ti.ItemDescription,
                                Quantity = ti.Quantity,
                                Amount = ti.Amount
                            }).ToList()
                        }).ToList(),
                        AdvancePaymentAmount = g.Key.AdvancePaymentAmount
                    })
                    .ToListAsync(cancellationToken);

                return Result.Success(paymentOverview);

            }

        }
    }
}
