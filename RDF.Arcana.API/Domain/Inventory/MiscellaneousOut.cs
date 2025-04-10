using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class MiscellaneousOut : BaseEntity
    {
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ICollection<MiscellaneousOutItems> MiscellaneousOutItems { get; set; }
    }
}
