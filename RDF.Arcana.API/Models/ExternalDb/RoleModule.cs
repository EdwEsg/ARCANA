using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class RoleModule
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public int? ModuleId { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? DateAdded { get; set; }
}
