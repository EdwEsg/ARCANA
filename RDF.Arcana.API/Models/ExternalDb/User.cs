using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public int? RoleId { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public int? AddedBy { get; set; }

    public int? DepartmentId { get; set; }
}
