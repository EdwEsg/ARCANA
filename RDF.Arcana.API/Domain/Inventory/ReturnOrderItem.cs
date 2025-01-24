using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class ReturnOrderItem : BaseEntity
    {
        public int ReturnOrderId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Bbd { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal RemainingQuantity { get; set; }
        public string Reason { get; set; }

        public virtual Items Item { get; set; }
        public virtual ReturnedOrder ReturnOrder { get; set; }


    }
}
