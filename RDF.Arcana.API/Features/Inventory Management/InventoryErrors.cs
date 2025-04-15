using RDF.Arcana.API.Common;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.GetPaymentOverview.GetPaymentOverviewResponse;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class InventoryErrors
    {
        public static Error NotFound(string code) => new("404", $"Not Found {code}");
        public static Error MoNotFound() => new("NotFound", "Move Order Not Found");
        public static Error ToNotFound() => new("NotFound", "Transfer Order Not Found");
        public static Error MoAlreadyExist() => new("AlreadyExist", "Move Order Already Exist in Database");
        public static Error CannotSync(string missingCodes) => new("Missing.Items",$"The following ItemCodes do not exist in the Arcana: {missingCodes}");
        public static Error NotYetTransacted() => new("Not.Transacted", "Move Order is not transacted yet OR it is not from GT or MT");
        public static Error WrongDeliver() => new("Wrong.Deliver", "The sum of Wrong Delivers should not exceed the Quantity or Actual Quantity");
        public static Error NotCdo() => new("Not.Cdo", "Reciver/To is not CDO");
        public static Error ItemNotFound(string itemCode) => new("Item.Not.Found", $"ItemCode '{itemCode}' not found in MoveOrderItems.");
        public static Error InsufficientQuantity(string itemCode, decimal requested, decimal available)
            => new("Insufficient.Quantity", $"Insufficient quantity for ItemCode '{itemCode}'. Requested: {requested}, Available: {available}.");
        public static Error Self() => new("Not.Self", "Cannot Transfer to self");
        public static Error NotUserCdo() => new("Not.Cdo", "User is not CDO");
        public static Error NoClientFound() => new("NotFound", "No Client Found");
        public static Error InvalidTranType() => new("InvalidTranType", "Invalid Transaction Type (Freebie/Sampling) only");
        public static Error NoReturnFound() => new("NotFound", "No Return Id Found");
        public static Error InvalidStatus() => new("Status.Not.Pending", "Status is Received");
        public static Error Unauthorized() => new("UserNotAunthorized", "User not Authorize");
        public static Error TransactionNotFound(string tran, string cdo) => new("NotFound", $"Transaction Id '{tran}'  Not Found for Cdo: {cdo}");
        public static Error TransactionItemNotFound(string tran, string cdo) => new("NotFound", $"TransactionItem Id '{tran}'  Not Found for Cdo: {cdo}");
        public static Error InvalidRemainingInventory(decimal rem, decimal quan, string tran) => new("CannotExceed", $"RemainingInv {rem} cannot exceed Quantity {quan} for TransactionItemId {tran}");
        //UpdateItemBBdV2
        public static Error ExcessQuantity(decimal quantity, decimal remaining, string itemCode)
            => new("Excess", $"Total input Quantity:{quantity} exceed Remaining Quantity:{remaining} for ItemCode: {itemCode} ");

        public static Error CannotFoundBbd(int? bbdId)
            => new("CannotFound", $"BbbId:{bbdId} cannot found");

        public static Error BbdDateNull(decimal quantity)
            => new("BbdDateBull", $"Bbd date should not be null because it has Quantity:{quantity}");

        public static Error ReturnOrderIdNotFound(string code) => new("404", $"Not Found Return Order Id: {code}");

        public static Error MiscOutError(int itemId, decimal quantity, decimal total)
            => new("CannotDeduct", $"Cannot MiscOut item {itemId}. Requested quantity {quantity} exceeds available quantity {total}");
    }
}
