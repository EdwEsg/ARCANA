using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses;

[Route("api/other-expenses-client"), ApiController]
public class GetOtherExpensesBalanceByClientId : ControllerBase
{
    private readonly IMediator _mediator;
    public GetOtherExpensesBalanceByClientId(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetOtherExpensesBalanceByClientIdQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetOtherExpensesBalanceByClientIdQuery : IRequest<Result>
    {
        public int ClientId { get; set;}
        public int OtherExpenseId { get; set; }
    }

    public class GetOtherExpensesBalanceByClientIdResult
    {
        public string BusinessName { get; set; }
        public decimal TotalBalance { get; set; }
        public IEnumerable<ExpensesRequest> ExpensesReq { get; set; }
        public class ExpensesRequest
        {
            public decimal RemainingBalance { get; set; }
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
            var otherExpenses = await _context.ExpensesRequests
                .Include(c => c.Client)
                .Where(oe => oe.ClientId == request.ClientId &&
                             oe.Status == Status.Approved &&
                             oe.OtherExpenseId == request.OtherExpenseId)
                .ToListAsync();

            var otherExpensesResults = otherExpenses.Select(oe => new GetOtherExpensesBalanceByClientIdResult.ExpensesRequest
            {
                RemainingBalance = oe.RemainingBalance
            }).ToList();

            var result = new GetOtherExpensesBalanceByClientIdResult
            {
                BusinessName = otherExpenses.First().Client.BusinessName,
                TotalBalance = otherExpensesResults.Sum(oe => oe.RemainingBalance),
                ExpensesReq = otherExpensesResults
            };

            return Result.Success(result);
        }
    }
}
