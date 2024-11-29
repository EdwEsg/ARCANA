using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class InventoryReceiving : BaseEntity
    {
        public int MoveOrderId { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCode { get; set; }
        public string Uom { get; set; }
        public decimal Quantity { get; set; }
        public decimal? QuantityReceived { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

        public bool IsActive { get; set; } = true;
        public int CreatedById { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public virtual User CreatedBy { get; set; }
    }
}
