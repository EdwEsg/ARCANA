using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class MeatType
{
    public int Id { get; set; }

    public string MeatType1 { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public int? AddedBy { get; set; }
}
