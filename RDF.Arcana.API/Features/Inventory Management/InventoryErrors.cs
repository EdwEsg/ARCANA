using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class InventoryErrors
    {
        public static Error NotFound() => new("NotFound", "Not Found");
    }
}
