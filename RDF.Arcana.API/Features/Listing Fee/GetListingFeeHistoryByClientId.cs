
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Listing_Fee.Errors;

namespace RDF.Arcana.API.Features.Listing_Fee
{
    [Route("api/get-lf-history-per-client"), ApiController]
    public class GetListingFeeHistoryByClientId : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetListingFeeHistoryByClientId(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetListingFeeHistoryByClientIdQuery query)
        {
            try
            {
                var clienti = await _mediator.Send(query);

                if (clienti.IsFailure)
                {
                    return BadRequest(clienti);
                }
                return Ok(clienti);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetListingFeeHistoryByClientIdQuery : IRequest<Result>
        {
            public int ClientId { get; set; }
        }

        public class GetListingFeeHistoryByClientIdResult
        {
            public string BusinessName { get; set; }
            public ICollection<LHistory> ListingHistory { get; set; }
            public class LHistory
            {
                public string PaymentType { get; set; }
                public decimal Amount { get; set; }
                public DateTime CreatedDate { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetListingFeeHistoryByClientIdQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetListingFeeHistoryByClientIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients
                    .Where(c => c.Id == request.ClientId)
                    .Select(c => c.BusinessName)
                    .FirstOrDefaultAsync(cancellationToken);

                if (client == null)
                {
                    return ListingFeeErrors.NoClientFound();
                }

                var paymentTransactions = await _context.PaymentTransactions
                    .Where(pt => pt.Transaction.ClientId == request.ClientId &&
                                 pt.PaymentMethod == PaymentMethods.ListingFee && 
                                 pt.Status != Status.Voided && 
                                 pt.Status != Status.Cancelled && 
                                 pt.TotalAmountReceived > 0)
                    .Select(pt => new GetListingFeeHistoryByClientIdResult.LHistory
                    {
                        PaymentType = "Payment",
                        Amount = pt.TotalAmountReceived,
                        CreatedDate = pt.DateReceived
                    })
                    .ToListAsync(cancellationToken);


                var cheques = await _context.Cheque
                    .Where(c => c.ClientId == request.ClientId &&
                                c.Amount > 0)
                    .Select(c => new GetListingFeeHistoryByClientIdResult.LHistory
                    {
                        PaymentType = "Check",
                        Amount = c.Amount,
                        CreatedDate = c.CreatedDate
                    })
                    .ToListAsync(cancellationToken);

                var listing = await _context.ListingFees
                    .Where(lf => lf.ClientId == request.ClientId &&
                        lf.Status == Status.Approved && 
                        lf.OriginalTotal > 0 )
                    .Select(lf => new GetListingFeeHistoryByClientIdResult.LHistory
                    {
                        PaymentType = "Add",
                        Amount = lf.OriginalTotal,
                        CreatedDate = lf.CratedAt
                    })
                    .ToListAsync(cancellationToken);


                var listingHistory = paymentTransactions.Concat(cheques).Concat(listing)
                    .OrderByDescending(l => l.CreatedDate)
                    .ToList();


                var result = new GetListingFeeHistoryByClientIdResult
                {
                    BusinessName = client,
                    ListingHistory = listingHistory
                };

                return Result.Success(result);
            }
        }
    }
}
