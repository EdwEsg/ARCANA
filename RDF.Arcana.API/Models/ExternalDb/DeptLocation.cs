using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class DeptLocation
{
    public int Id { get; set; }

    public int? DepartmentId { get; set; }

    public int? LocationId { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? DateAdded { get; set; }
}
