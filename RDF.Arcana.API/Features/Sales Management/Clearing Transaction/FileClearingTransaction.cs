using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using System.Security.Claims;

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

	public class FiledClearingTransctionCommand : IRequest<Result>
	{
        public int AddedBy { get; set; }
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
            //Admin and Sir Roger
            if (request.AddedBy != 1 && request.AddedBy != 17)
            {
                return ClearingErrors.Unauthorized();
            }

            if (request.PaymentRecords is null)
            {
                return ClearingErrors.NotFound();
            }

                foreach (var paymentRecord in request.PaymentRecords)
                {
                    var paymentTransaction = await _context.PaymentTransactions
                        .Where(pt => pt.PaymentRecordId == paymentRecord.PaymentRecordId && 
                                     pt.PaymentMethod == paymentRecord.PaymentMethod &&
                                     pt.PaymentAmount == paymentRecord.PaymentAmount)
                        .ToListAsync(cancellationToken);
                    
                    if (paymentTransaction is null)
                    {
                        return ClearingErrors.NotFound();
                    }

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
