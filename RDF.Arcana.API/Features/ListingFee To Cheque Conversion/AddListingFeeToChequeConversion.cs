//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;

//namespace RDF.Arcana.API.Features.ListingFee_To_Cheque_Conversion
//{
//    public class AddListingFeeToChequeConversion
//    {
//        public class AddListingFeeToChequeConversionCommand : IRequest<Result>
//        {
//            public int ListingFeeId { get; set; }
//            public decimal AmountToConvert { get; set; }
//        }

//        public class Handler : IRequestHandler<AddListingFeeToChequeConversionCommand, Result>
//        {
//            private readonly ArcanaDbContext _context;
//            public Handler(ArcanaDbContext context)
//            {
//                _context = context;
//            }

//            public async Task<Result> Handle(AddListingFeeToChequeConversionCommand request, CancellationToken cancellationToken)
//            {
//                var listingFeeId = await _context.ListingFees
//                    .Where(lf => lf.Status == Status.Approved && lf.Id == request.ListingFeeId)
//                    .OrderByDescending(lf => lf.Total)
//                    .ToListAsync(cancellationToken);

//                if (listingFeeId is null)
//                {
//                    return ListingFeeToChequeConversionError.NotFound();
//                }

//                var amountToConvert = request.AmountToConvert;
//                listingFeeId.First().Total -= amountToConvert;

//            }
//        }
//    }
//}
