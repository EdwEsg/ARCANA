using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.Inventory
{
    public class ReturnedOrder : BaseEntity
    {
        public int ClientId { get; set; }
        public decimal TotalReturn { get; set; }
        public decimal TotalExchange { get; set; }

        public bool IsActive { get; set; } = true;
        public int CreatedbyId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual Clients Client { get; set; }
        public virtual User CreatedBy { get; set; }

        public ICollection<ReturnOrderItem> ReturnOrderItems { get; set; }
        public ICollection<ReplaceOrderItem> ReplaceOrderItems { get; set; }

    }
}
