using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class Customer
{
    public int Id { get; set; }

    public string CustomerCode { get; set; }

    public string CustomerName { get; set; }

    public string Address { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public int? AddedBy { get; set; }

    public int? AreaId { get; set; }

    public int? BusinessCategoryId { get; set; }

    public string Org { get; set; }
}
