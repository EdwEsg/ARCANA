using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions
{
    public class CancelTransactions
    {
        public class CancelTransactionsCommand : IRequest<Result>
        {
            public List<int> TransactionId { get; set; }
            public string Reason { get; set; }
        }

        public class Handler : IRequestHandler<CancelTransactionsCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<Result> Handle(CancelTransactionsCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
