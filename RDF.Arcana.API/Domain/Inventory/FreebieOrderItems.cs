using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class FreebieOrderItems : BaseEntity
    {
        public int FreebieOrderId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public bool IsActive { get; set; } = true;
        public int? MoveOrderId { get; set; }
        public int? TransferOrderId { get; set; }

        public virtual FreebieOrder FreebieOrder { get; set; }
        public virtual MoveOrder MoveOrder { get; set; }
        public virtual TransferOrder TransferOrder { get; set; }
    }
}
