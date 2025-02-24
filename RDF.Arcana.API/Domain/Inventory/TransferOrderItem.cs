using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class TransferOrderItem : BaseEntity
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Uom { get; set; }
        public decimal? Quantity { get; set; }
        public string ProductionDate { get; set; }
        public int? MoveId { get; set; }
        public int TransferOrderId { get; set; }

        public bool IsActive { get; set; } = true;
        public int CreatedById { get; set; }
        public decimal? RemainingQuantity { get; set; }
        public int ItemId { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public string Bbd { get; set; }

        public virtual Items Item { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual TransferOrder TransferOrder { get; set; }
    }
}
