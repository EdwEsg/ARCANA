using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class TransactionBbd : BaseEntity
    {
        public int CreatedById { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual User CreatedBy { get; set; }
        public virtual ICollection<TransactionItemBbd> TransactionItemBbds { get; set; }
    }
}
