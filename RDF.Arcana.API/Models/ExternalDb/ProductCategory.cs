using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class ProductCategory
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string ProductCategory1 { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public int? AddedBy { get; set; }
}
