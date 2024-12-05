using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class InventoryErrors
    {
        public static Error MoNotFound() => new("NotFound", "Move Order Not Found");
        public static Error MoAlreadyExist() => new("AlreadyExist", "Move Order Already Exist in Database");
        public static Error CannotSync(string missingCodes) => new("Missing.Items",$"The following ItemCodes do not exist in the Arcana: {missingCodes}");
        public static Error NotYetTransacted() => new("Not.Transacted", "Move Order is not transacted yet");
        public static Error WrongDeliver() => new("Wrong.Deliver", "The sum of Wrong Delivers should not exceed the Quantity or Actual Quantity");
    }
}
