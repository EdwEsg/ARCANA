using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class MoveOrderItem
{
    public int Id { get; set; }

    public int? MoveId { get; set; }

    public int? ItemId { get; set; }

    public double? Quantity { get; set; }

    public DateTime? Date { get; set; }

    public double? ActualQuantity { get; set; }

    public double? Slab { get; set; }

    public string Reason { get; set; }

    public string FarmSource { get; set; }

    public string ProductionDate { get; set; }

    public string ItemReference { get; set; }
}
