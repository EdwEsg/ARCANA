using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;

[Route("api/cancel-transaction"), ApiController]
public class CancelTransactions : ControllerBase
{
    private readonly IMediator _mediator;
    public CancelTransactions(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    public async Task<IActionResult> Cancel([FromBody] CancelTransactionsCommand command)
    {
        try
        {

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    public class CancelTransactionsCommand : IRequest<Result>
    {
        public int? TransactionId { get; set; }
        public int? PaymentRecordsId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<CancelTransactionsCommand, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CancelTransactionsCommand request, CancellationToken cancellationToken)
        {
            if (request.TransactionId is not null && request.PaymentRecordsId is not null)
            {
                return TransactionErrors.InvalidInputs();
            }

            else if (request.TransactionId is not null)
            {
                var transaction = await _context.Transactions
                    .Include(ts => ts.TransactionSales)
                    .FirstOrDefaultAsync(t => t.Status == Status.Pending
                        && t.Id == request.TransactionId
                        && t.TransactionSales.TotalAmountDue == t.TransactionSales.RemainingBalance, cancellationToken);

                if (transaction is null)
                {
                    return TransactionErrors.NotFound();
                }

                transaction.Status = Status.Cancelled;
                transaction.TransactionSales.Remarks = request.Reason;
                transaction.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
