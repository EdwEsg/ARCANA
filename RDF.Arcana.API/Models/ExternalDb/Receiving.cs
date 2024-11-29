using System;
using System.Collections.Generic;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class Receiving
{
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public string Description { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int? AddedBy { get; set; }

    public bool? Status { get; set; }

    public string Reference { get; set; }

    public string AccountTitle { get; set; }

    public string CompanyCode { get; set; }

    public string DepartmentCode { get; set; }

    public string LocationCode { get; set; }

    public string AccountCode { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string WarehouseCode { get; set; }

    public string Reason { get; set; }

    public DateTime? ReceivingDate { get; set; }
}
