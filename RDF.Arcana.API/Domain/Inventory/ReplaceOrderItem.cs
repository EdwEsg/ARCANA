using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class ReplaceOrderItem : BaseEntity
    {
        public int ReturnOrderId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Bbd { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Items Item { get; set; }
        public virtual ReturnedOrder ReturnOrder { get; set; }
    }
}
