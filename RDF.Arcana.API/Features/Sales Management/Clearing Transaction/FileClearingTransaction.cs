using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction;
[Route("api/clearing-transaction"), ApiController]
public class FileClearingTransaction : ControllerBase
{
	private readonly IMediator _mediator;

	public FileClearingTransaction(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPatch("filing")]
	public async Task<IActionResult> File([FromBody] FiledClearingTransctionCommand command)
	{
		var result = await _mediator.Send(command);
		return result.IsFailure ? BadRequest(result) : Ok(result);

	}

	public class FiledClearingTransctionCommand : IRequest<Result>
	{
        public ICollection<PaymentRecord> PaymentRecords { get; set; }
        public class PaymentRecord
        {
            public int PaymentRecordId { get; set; }
            public string PaymentMethod { get; set; }
            public decimal PaymentAmount { get; set; }
        }
    }

	public class Handler : IRequestHandler<FiledClearingTransctionCommand, Result>
	{
		private readonly ArcanaDbContext _context;

		public Handler(ArcanaDbContext context)
		{
			_context = context;
		}

		public async Task<Result> Handle(FiledClearingTransctionCommand request, CancellationToken cancellationToken)
        {
                foreach (var paymentRecord in request.PaymentRecords)
                {
                    var paymentTransaction = await _context.PaymentTransactions
                        .Where(pt => pt.PaymentRecordId == paymentRecord.PaymentRecordId && 
                                     pt.PaymentMethod == paymentRecord.PaymentMethod &&
                                     pt.PaymentAmount == paymentRecord.PaymentAmount)
                        .ToListAsync(cancellationToken);

                    foreach (var payment in paymentTransaction)
                    {
                        payment.Status = Status.Cleared;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }

                
                return Result.Success();
                
        }
	}
}
