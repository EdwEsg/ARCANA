using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using System.Security.Claims;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.AddNewPaymentTransaction.AddNewPaymentTransactionCommand;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.VoidPaymentTransaction;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
	[Microsoft.AspNetCore.Mvc.Route("api/clearing-transaction"), ApiController]
	public class VoidClearingTransaction : ControllerBase
	{
		private readonly IMediator _mediator;
		public VoidClearingTransaction(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPut("void")]
		public async Task<IActionResult> Void([FromBody] VoidClearingTransactionCommand command)
		{
            try
            {

                if (User.Identity is ClaimsIdentity identity
                    && IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    command.AddedBy = userId;
                }

                var result = await _mediator.Send(command);

                return result.IsFailure ? BadRequest(result) : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		public class VoidClearingTransactionCommand : IRequest<Result>
		{
            public string Reason { get; set; }
            public int PaymentRecordId { get; set; }
            public string PaymentMethod { get; set; }
            public decimal PaymentAmount { get; set; }
            public int AddedBy { get; set; }
            public int? ModifiedBy { get; set; }
        }

		public class Handler : IRequestHandler<VoidClearingTransactionCommand, Result>
		{
			private readonly ArcanaDbContext _context;

			public Handler(ArcanaDbContext context)
			{
				_context = context;

			}

			public async Task<Result> Handle(VoidClearingTransactionCommand request, CancellationToken cancellationToken)
			{
				var paymentTransactions = await _context.PaymentTransactions
                    .Include(t => t.Transaction)
                        .ThenInclude(ts => ts.TransactionSales)
					.Where(pt => pt.PaymentRecordId == request.PaymentRecordId &&
								 pt.PaymentMethod == request.PaymentMethod && 
								 pt.PaymentAmount == request.PaymentAmount)
					.ToListAsync(cancellationToken);

                if (paymentTransactions is null)
                {
                    return ClearingErrors.NotFound();
                }

                foreach (var payment in paymentTransactions)
				{
                    if (payment.PaymentMethod == PaymentMethods.ListingFee)
                    {
                        var listingFee = _context.ListingFees.Where(lf => lf.ClientId == payment.Transaction.ClientId &&
                                         lf.Status == Status.Approved &&
                                         lf.IsActive)
                                         .OrderBy(lf => lf.CratedAt)
                        .First();

                        listingFee.Total += payment.TotalAmountReceived;

                    }

                    if (payment.PaymentMethod == PaymentMethods.Others)
                    {
                        var otherExpenses = _context.ExpensesRequests.Where(oe => oe.ClientId == payment.Transaction.ClientId &&
                                            oe.Status == Status.Approved &&
                                            oe.OtherExpenseId == payment.ExpensesRequestId)
                        .OrderBy(oe => oe.CreatedAt)
                                            .First();

                        otherExpenses.RemainingBalance += payment.TotalAmountReceived;
                    }

                    if (payment.PaymentMethod == PaymentMethods.AdvancePayment)
                    {
                        var advancePayment = _context.AdvancePayments.Where(ap => ap.ClientId == payment.Transaction.ClientId &&
                                             ap.IsActive)
                        .OrderBy(ap => ap.CreatedAt)
                                             .First();

                        advancePayment.RemainingBalance += payment.TotalAmountReceived;
                    }

                    payment.Status = Status.Voided;
                    payment.Transaction.Status = Status.Pending;
                    payment.Transaction.TransactionSales.RemainingBalance += payment.TotalAmountReceived;
                    payment.Reason = request.Reason;

                    await _context.SaveChangesAsync(cancellationToken);
                }


                return Result.Success();
            }

		}
	}
}
