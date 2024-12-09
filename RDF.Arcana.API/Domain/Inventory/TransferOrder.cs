using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class TransferOrder : BaseEntity
    {
        public int To { get; set; }
        public string TransactionType { get; set; }
        public decimal TotalQuantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransferType { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int CreatedById { get; set; }
        public int? ModifiedBy { get; set; }
        public string Status { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ICollection<TransferOrderItem> TransferOrderItems { get; set; }
    }
}
