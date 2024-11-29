using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class ReceiptItem
{
    public int Id { get; set; }

    public int? ReceiptId { get; set; }

    public int? ItemId { get; set; }

    public double? Quantity { get; set; }

    public DateTime? Date { get; set; }

    public double? Slab { get; set; }

    public string FarmSource { get; set; }

    public string ProductionDate { get; set; }

    public string ItemReference { get; set; }
}
