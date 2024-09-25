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
        public int TransactionId { get; set; }
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
                var transaction = await _context.Transactions
                    .Include(ts => ts.TransactionSales)
                    .Include(pt => pt.PaymentTransactions)
                    .FirstOrDefaultAsync(t => (t.Status == Status.Pending || t.Status == Status.Paid)
                        && t.Id == request.TransactionId, cancellationToken);

                if (transaction is null)
                {
                    return TransactionErrors.NotFound();
                }
                 
                if (transaction.TransactionSales.TotalAmountDue != transaction.TransactionSales.RemainingBalance)
                {
                    var paymentTransaction = transaction.PaymentTransactions
                        .Where(pt => pt.PaymentMethod == PaymentMethods.ListingFee ||
                                     pt.PaymentMethod == PaymentMethods.Others ||
                                     pt.PaymentMethod == PaymentMethods.AdvancePayment)
                        .ToList();
                }

                transaction.Status = Status.Cancelled;
                transaction.TransactionSales.Remarks = request.Reason;
                transaction.UpdatedAt = DateTime.Now;
            

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
