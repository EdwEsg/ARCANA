//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;
//using RDF.Arcana.API.Domain;

//namespace RDF.Arcana.API.Features.ListingFee_To_Cheque_Conversion
//{
//    public class AddListingFeeToChequeConversion
//    {
//        public class AddListingFeeToChequeConversionCommand : IRequest<Result>
//        {
//            public int ClientId { get; set; }
//            public string Bank { get; set; }
//            public string ChequeNo { get; set; }
//            public string ChequeDate { get; set; }
//            public string VoucherNo { get; set; }
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
//                var listingFees = await _context.ListingFees
//                    .Where(lf => lf.Status == Status.Approved && 
//                                 lf.ClientId == request.ClientId &&
//                                 lf.IsActive &&
//                                 lf.Total > 0)
//                    .OrderBy(cr => cr.CratedAt)
//                    .ToListAsync(cancellationToken);

//                if (listingFees is null)
//                {
//                    return ListingFeeToChequeConversionError.NotFound();
//                }

//                var listingFeesTotal = listingFees.Sum(t => t.Total);

//                var amountToConvert = request.AmountToConvert;

//                var checkBalance = listingFeesTotal - amountToConvert;

//                if (checkBalance < 0)
//                {
//                    return ListingFeeToChequeConversionError.Insufficient();
//                }

//                foreach (var fee in listingFees)
//                {
//                    var remainingBalance = fee.Total - amountToConvert;

//                    if (remainingBalance < 0) 
//                    {
//                        fee.Total = 0;
//                        amountToConvert = Math.Abs(remainingBalance);
                        
//                    }
//                    else
//                    {
//                        fee.Total = remainingBalance;
//                        await _context.SaveChangesAsync(cancellationToken);
//                        break;
//                    }
                    
//                    await _context.SaveChangesAsync(cancellationToken);
//                }

//                var cheque = new Cheque
//                {
//                    ClientId = request.ClientId,
//                    ListingFeeId = listingFees.First().Id,
//                    PaymentTransactionId = null,
//                    Bank = request.Bank,
//                    ChequeNo = request.ChequeNo,
//                    ChequeDate = request.ChequeDate,
//                    VoucherNo = request.VoucherNo,
//                    Amount = request.AmountToConvert,
//                    RemainingBalance = 0,


//                };

//            }
//        }
//    }
//}
