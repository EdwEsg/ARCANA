using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class TransactionItemBbd : BaseEntity
    {
        public int TransactionItemsId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Bbd { get; set; }
        public virtual TransactionItems TransactionItems { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal RemainingQuantity { get; set; }

    }
}
