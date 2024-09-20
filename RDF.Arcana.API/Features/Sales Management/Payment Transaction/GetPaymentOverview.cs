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
			public string ReferenceNo { get; set; }
            public string PaymentMethod { get; set; }
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
                public int Quantity { get; set; }
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

                var paymentRecords = await _context.PaymentRecords
                    .Where(pr => pr.PaymentTransactions.Any(pt => pt.ReferenceNo == request.ReferenceNo))
                    .Include(pr => pr.PaymentTransactions)
                        .ThenInclude(pt => pt.Transaction)
                            .ThenInclude(t => t.TransactionItems)
                                .ThenInclude(ti => ti.Item)
                    .Include(pr => pr.PaymentTransactions)
                        .ThenInclude(pt => pt.AddedByUser)
                    .ToListAsync(cancellationToken);

                var advancePaymentAmount = await _context.AdvancePayments
                    .FirstOrDefaultAsync(x => x.ChequeNo == request.ReferenceNo, cancellationToken: cancellationToken);

                var paymentOverview = paymentRecords
                    .Select(pr => new GetPaymentOverviewResponse
                    {
                        PaymentMethod = pr.PaymentTransactions.First().PaymentMethod,
                        PaymentChannel = pr.PaymentTransactions.First().BankName,
                        CreatedAt = pr.PaymentTransactions.First().ChequeDate.ToString("MM-dd-yyyy"),
                        AddedBy = pr.PaymentTransactions.First().AddedByUser.Fullname,
                        TotalAmount = pr.PaymentTransactions.Sum(pt => pt.PaymentAmount),
                        ModeOfPayment = pr.PaymentTransactions.First().PaymentMethod,
                        Reason = pr.PaymentTransactions.First().Reason,

                        ReceiptNo = pr.ReceiptNo,
                        ReceiptAttachment = pr.Receipt,

                        Attachment = (request.AddedBy == 17 || request.AddedBy == 1) ? pr.PaymentTransactions.FirstOrDefault()?.WithholdingAttachment : null,
                        WithholdingNo = (request.AddedBy == 17 || request.AddedBy == 1) ? pr.PaymentTransactions.FirstOrDefault()?.WithholdingNo : null,

                        Transactions = pr.PaymentTransactions.Select(pt => new GetPaymentOverviewResponse.Transaction
                        {
                            PaymentTransactionId = pt.Id,
                            InvoiceType = pt.Transaction.InvoiceType,
                            InvoiceNo = pt.Transaction.InvoiceNo,
                            PaymentAmount = pt.TotalAmountReceived,
                            TransactionItems = pt.Transaction.TransactionItems.Select(ti => new GetPaymentOverviewResponse.TransactionItem
                            {
                                ItemCode = ti.Item.ItemCode,
                                ItemDescription = ti.Item.ItemDescription,
                                Quantity = ti.Quantity,
                                Amount = ti.Amount
                            }).ToList()
                        }).ToList(),

                        AdvancePaymentAmount = advancePaymentAmount?.AdvancePaymentAmount
                    })
                    .ToList();

                return Result.Success(paymentOverview);

            }
        }
	}
}
