using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace Arcana.API.Features.Sales_Management.Clearing.Transaction
{
    [Route("api/sandbox")]
    [ApiController]
    public class GetAllRemittance : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllRemittance(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllRemittanceQuery query)
        {
            try
            {
                var transactions = await _mediator.Send(query);

                Response.AddPaginationHeader(transactions.CurrentPage, transactions.PageSize, transactions.TotalCount,
                    transactions.TotalPages, transactions.HasNextPage, transactions.HasPreviousPage);

                var result = new
                {
                    transactions,
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage
                };

                var successResult = Result.Success(result);
                return Ok(successResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class GetAllRemittanceQuery : UserParams, IRequest<PagedList<GetAllRemittanceResult>>
    {
        public int TransactionId { get; set; }
    }

    public class GetAllRemittanceResult
    {
        public string BusinessName { get; set; }
        public string Status { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }
    }

    public class Handler : IRequestHandler<GetAllRemittanceQuery, PagedList<GetAllRemittanceResult>>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRemittanceResult>> Handle(GetAllRemittanceQuery request, CancellationToken cancellationToken)
        {
            var query = from transact in _context.Transactions
                        where transact.Status == Status.Paid
                        select new GetAllRemittanceResult
                        {
                            BusinessName = transact.Client.BusinessName,
                            Status = transact.Status,
                            InvoiceNo = transact.InvoiceNo,
                            InvoiceType = transact.InvoiceType,
                        };

            return await PagedList<GetAllRemittanceResult>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}
