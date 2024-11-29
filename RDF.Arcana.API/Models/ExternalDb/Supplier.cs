using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class Supplier
{
    public int Id { get; set; }

    public string SupplierCode { get; set; }

    public string SupplierName { get; set; }

    public string Address { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public int? AddedBy { get; set; }
}
