using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class MiscellaneousOutItems : BaseEntity
    {
        public int MiscellaneousOutId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public string Bbd { get; set; }
        public bool IsActive { get; set; } = true;
        public string Reason { get; set; }

        public virtual Items Item { get; set; }
        public virtual MiscellaneousOut MiscellaneousOut { get; set; }
    }
}
