using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class CancellationReason
{
    public int Id { get; set; }

    public string CancellationReason1 { get; set; }

    public bool? Status { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? DateAdded { get; set; }
}
