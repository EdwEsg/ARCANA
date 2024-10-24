using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;


namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
	[Route("api/clearing-transaction")]
	public class ReturnForFilingPaymentTransaction : ControllerBase
	{
		private readonly IMediator _mediator;

		public ReturnForFilingPaymentTransaction(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPatch("filing/return")]
		public async Task<IActionResult> ReturnForFiling([FromBody] ReturnForFilingPaymentTransactionCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsFailure ? BadRequest(result) : Ok(result);
		}

		public class ReturnForFilingPaymentTransactionCommand : IRequest<Result>
		{
            public int AddedBy { get; set; }
            public ICollection<ReturnPaymentRecordTo> PaymentRecords { get; set; }
            public class ReturnPaymentRecordTo
            {
                public int PaymentRecordId { get; set; }
                public string PaymentMethod { get; set; }
                public decimal PaymentAmount { get; set; }
            }
        }

		//public class Handler : IRequestHandler<ReturnForFilingPaymentTransactionCommand, Result>
		//{
		//	private readonly ArcanaDbContext _context;

		//	public Handler(ArcanaDbContext context)
		//	{
		//		_context = context;
		//	}

		//	public async Task<Result> Handle(ReturnForFilingPaymentTransactionCommand request, CancellationToken cancellationToken)
		//	{
  //              //Admin and Sir Roger
  //              if (request.AddedBy != 1 && request.AddedBy != 17)
  //              {
  //                  return ClearingErrors.Unauthorized();
  //              }

  //              if (request.PaymentRecords is null)
  //              {
  //                  return ClearingErrors.NotFound();
  //              }

  //              foreach (var paymentRecord in request.PaymentRecords)
		//		{
		//			var paymentTransaction = await _context.PaymentTransactions
		//				.Where(pt => pt.PaymentRecordId == paymentRecord.PaymentRecordId &&
		//							 pt.PaymentMethod == paymentRecord.PaymentMethod &&
		//							 pt.PaymentAmount == paymentRecord.PaymentAmount)
		//				.ToListAsync(cancellationToken);

  //                  foreach (var payment in paymentTransaction)
  //                  {
  //                      payment.Status = Status.ForClearing;
		//				payment.
  //                      await _context.SaveChangesAsync(cancellationToken);
  //                  }
  //              }

		//		// Check if any payment transactions were found
		//		if (request.PaymentTransactionIds.Any())
		//		{
		//			return Result.Success();
		//		}
		//		else
		//		{
		//			return ClearingErrors.NotFound();
		//		}
		//	}
		//}
	}
}
