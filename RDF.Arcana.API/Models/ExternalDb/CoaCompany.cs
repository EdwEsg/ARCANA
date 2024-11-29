using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class CoaCompany
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Company { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public int? AddedBy { get; set; }
}
