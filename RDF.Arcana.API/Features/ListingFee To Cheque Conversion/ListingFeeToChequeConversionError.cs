using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.ListingFee_To_Cheque_Conversion
{
    public class ListingFeeToChequeConversionError
    {
        public static Error NotFound() => new("NotFound", "Listing Fee Not Found/Check if Status is Approved");
        public static Error Insufficient() => new("Insufficient", "Listing Fee Insufficient Balance");
    }
}
