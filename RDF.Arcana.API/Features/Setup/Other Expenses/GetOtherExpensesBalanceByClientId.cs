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
    }

    public class GetOtherExpensesBalanceByClientIdResult
    {
        public string BusinessName { get; set; }
        public ICollection<OtherEx> Others { get; set; }
        public class OtherEx
        {
            public string OtherExpenseName { get; set; }
            public decimal Balance { get; set; }
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
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                return OtherExpensesErrors.NotFound();
            }


            var others = await _context.OtherExpenses
                .Select(oe => new GetOtherExpensesBalanceByClientIdResult.OtherEx
                {
                    OtherExpenseName = oe.ExpenseType,
                    Balance = _context.ExpensesRequests
                        .Where(er => er.OtherExpenseId == oe.Id &&
                                     er.ClientId == request.ClientId &&
                                     er.Status == Status.Approved)
                        .Sum(er => (decimal?)er.RemainingBalance) ?? 0
                })
                .ToListAsync(cancellationToken);

            var result = new GetOtherExpensesBalanceByClientIdResult
            {
                BusinessName = client.BusinessName,
                Others = others
            };

            return Result.Success(result);
        }
    }


}
