using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class MoveOrderItem : BaseEntity
    {
        public int MoveOrderId { get; set; }
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal? ActualQuantity { get; set; }
        public string ProductionDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int ItemId { get; set; }
        public int UomId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Reason { get; set; }
        public decimal? RemainingQuantity { get; set; }

        public virtual Uom Uom { get; set; }
        public virtual Items Item { get; set; }
        public virtual MoveOrder MoveOrder { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
