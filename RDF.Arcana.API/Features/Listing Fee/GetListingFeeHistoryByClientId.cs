using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Listing_Fee
{
    public class GetListingFeeHistoryByClientId
    {
        public class GetListingFeeHistoryByClientIdQuery : IRequest<Result>
        {
            public int ClientId { get; set; }
        }

        public class GetListingFeeHistoryByClientIdResult
        {
            public string BusinessName { get; set; }
            public decimal RemainingBalance { get; set; }
            public ICollection<LFHistory> ListingHistory { get; set; }
            public class LFHistory
            {
                public string PaymentType { get; set; }
                public decimal Amount { get; set; }
                public DateTime CreatedDate { get; set; }
            }
        }

        //public class Handler : IRequestHandler<GetListingFeeHistoryByClientIdQuery, Result>
        //{
        //    private readonly ArcanaDbContext _context;
        //    public Handler(ArcanaDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<Result> Handle(GetListingFeeHistoryByClientIdQuery request, CancellationToken cancellationToken)
        //    {
        //        var listingFee = await _context.ListingFees
        //            .Where(lf => lf.ClientId == request.ClientId &&
        //                     lf.Status == Status.Approved)
        //            .Select(x => new
        //            {
        //                BusinessName = x.Client.BusinessName,
        //                TotalLf = x.Total,

        //            });
        //    }
        //}
    }
}
