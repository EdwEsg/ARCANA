using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class MoveOrder : BaseEntity
    {
        public string CustomerName { get; set; }
        public string Route { get; set; }
        public string Details { get; set; }
        public string Area { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? MoveOrderIdExternal { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int CreatedById { get; set; }
        public int? ModifiedBy { get; set; }
        public string Type { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ICollection<MoveOrderItem> MoveOrderItems { get; set; }
    }
}
