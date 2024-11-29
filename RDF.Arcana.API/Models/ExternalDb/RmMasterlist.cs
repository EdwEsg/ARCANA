using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class RmMasterlist
{
    public int Int { get; set; }

    public string ItemCode { get; set; }

    public string ItemDescription { get; set; }

    public int? UomId { get; set; }

    public int? CategoryId { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public int? AddedBy { get; set; }

    public decimal? Conversion { get; set; }
}
