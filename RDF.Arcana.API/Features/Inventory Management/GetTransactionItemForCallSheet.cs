using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-transaction-item-callsheet")]
    public class GetTransactionItemForCallSheet : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetTransactionItemForCallSheet(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTransactionItemForCallSheetQuery query)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    query.AccessBy = userId;
                }

                var result = await _mediator.Send(query);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetTransactionItemForCallSheetQuery : IRequest<Result> { public int AccessBy { get; set; } }

        public class GetTransactionItemForCallSheetResult
        {
            public int TransactionItemId { get; set; }
            public decimal Quantity { get; set; }
            public decimal RemainingQuantity { get; set; }
        }

        public class Handler : IRequestHandler<GetTransactionItemForCallSheetQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetTransactionItemForCallSheetQuery request, CancellationToken cancellationToken)
            {
                var transactionItems = await _context.TransactionItems
                    .Where(t => t.AddedBy == request.AccessBy && t.RemainingQuantity > 0)
                    .ToListAsync(cancellationToken);

                var result = transactionItems.Select(t => new GetTransactionItemForCallSheetResult
                {
                    TransactionItemId = t.Id,
                    Quantity = t.Quantity,
                    RemainingQuantity = t.RemainingQuantity,
                }).OrderByDescending(t => t.TransactionItemId);

                return Result.Success(result);

            }
        }
    }
}
