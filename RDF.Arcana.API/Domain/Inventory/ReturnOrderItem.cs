using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class ReturnOrderItem : BaseEntity
    {
        public int ReturnOrderId { get; set; }
        public int ItemId { get; set; }
    }
}
