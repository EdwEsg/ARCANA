using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class InventoryErrors
    {
        public static Error MoNotFound() => new("NotFound", "Move Order Not Found");
        public static Error ToNotFound() => new("NotFound", "Transfer Order Not Found");
        public static Error MoAlreadyExist() => new("AlreadyExist", "Move Order Already Exist in Database");
        public static Error CannotSync(string missingCodes) => new("Missing.Items",$"The following ItemCodes do not exist in the Arcana: {missingCodes}");
        public static Error NotYetTransacted() => new("Not.Transacted", "Move Order is not transacted yet");
        public static Error WrongDeliver() => new("Wrong.Deliver", "The sum of Wrong Delivers should not exceed the Quantity or Actual Quantity");
        public static Error NotCdo() => new("Not.Cdo", "Reciver/To is not CDO");
        public static Error ItemNotFound(string itemCode) => new("Item.Not.Found", $"ItemCode '{itemCode}' not found in MoveOrderItems.");
        public static Error InsufficientQuantity(string itemCode, decimal requested, decimal available)
            => new("Insufficient.Quantity", $"Insufficient quantity for ItemCode '{itemCode}'. Requested: {requested}, Available: {available}.");
        public static Error Self() => new("Not.Self", "Cannot Transfer to self");
        public static Error NotUserCdo() => new("Not.Cdo", "User is not CDO");
        public static Error NoClientFound() => new("NotFound", "No Client Found");
        public static Error InvalidTranType() => new("InvalidTranType", "Invalid Transaction Type (Freebie/Sampling) only");
    }
}
