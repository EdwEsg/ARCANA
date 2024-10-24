using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    public class ClearingErrors
    {
        public static Error NotFound() =>
        new("Transaction.NotFound", "Transaction not found");

        public static Error AlreadyExist() =>
        new("Already.Exist", "Transaction Already Cleared");

        public static Error Cleared() =>
        new("Cleared", "Transaction is Already Cleared");

        public static Error Voided() =>
        new("Voided", "Transaction is Already Voided");

        public static Error NotFoundReferece() =>
        new("Reference.NotFound", "Please input ChequeNo, ReferenceNo, or TransactionNo");

        public static Error Unauthorized() => new("Unauthorized", "You do not have permission to do this action (Sir Roger ONLY!)");
    }
}
