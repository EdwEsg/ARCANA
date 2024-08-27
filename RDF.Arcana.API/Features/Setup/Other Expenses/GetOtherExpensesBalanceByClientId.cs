using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses;

[Route("api/other-expenses"), ApiController]
public class GetOtherExpensesBalanceByClientId : ControllerBase
{

    public class GetOtherExpensesBalanceByClientIdQuery : IRequest<Result>
    {
        public int ClientId { get; set;}
    }

    public class GetOtherExpensesBalanceByClientIdResult
    {
        public string BusinessName { get; set; }
        public decimal TotalBalance { get; set; }
        public IEnumerable<Expenses> OtherExpeneses { get; set; }
        public class Expenses
        {
            public DateTime CreatedAt { get; set; }
            public DateTime ApprovalDate { get; set; }
            public string RequestedByFullname { get; set; }
            public decimal Total { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetOtherExpensesBalanceByClientIdQuery, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(GetOtherExpensesBalanceByClientIdQuery request, CancellationToken cancellationToken)
        {
            var otherExpenses = await _context.Expenses
                .Include(c => c.Client)
                .Include(u => u.AddedByUser)
                .Where(oe => oe.ClientId == request.ClientId &&
                             oe.Status == Status.Approved)
                .ToListAsync();

            var otherExpensesResults = otherExpenses.Select(oe => new GetOtherExpensesBalanceByClientIdResult.Expenses
            {
                CreatedAt = oe.CreatedAt,
                ApprovalDate = oe.UpdatedAt,
                RequestedByFullname = oe.AddedByUser.Fullname,
                Total = oe.Total
            }).ToList();

            var result = new GetOtherExpensesBalanceByClientIdResult
            {
                BusinessName = otherExpenses.First().Client.BusinessName,
                TotalBalance = otherExpensesResults.Sum(oe => oe.Total),
                OtherExpeneses = otherExpensesResults
            };

            return Result.Success(result);
        }
    }
}
