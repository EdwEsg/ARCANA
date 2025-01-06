using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class FreebieOrder : BaseEntity
    {
        public int ClientId { get; set; }
        public string TransactionType { get; set; }
        public decimal TotalQuantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Clients Client { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<FreebieOrderItems> FreebieOrderItems { get; set; }

    }
}
