using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class SalesReturn : BaseEntity
    {
        public int ReturnedOrderId { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingBalance { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public int CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ReturnedOrder ReturnedOrder { get; set; }
        public virtual Clients Client { get; set; }
    }
}
