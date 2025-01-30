using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Domain.Inventory;

namespace RDF.Arcana.API.Domain;

public class TransactionItems : BaseEntity
{
    public int TransactionId { get; set; }
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice  { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public int AddedBy { get; set; }
    public int? PriceModeId { get; set; }
    public decimal RemainingQuantity { get; set; }

    public virtual Transactions Transaction { get; set; }
    public virtual Items Item { get; set; }
    public virtual User AddedByUser { get; set; }
    public virtual PriceMode PriceMode { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<TransactionItemBbd> TransactionItemBbd{ get; set; }
}
