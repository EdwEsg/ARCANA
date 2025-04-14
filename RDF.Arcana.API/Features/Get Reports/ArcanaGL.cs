//Inventoriables

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Authenticate.AuthXApi;
using System.Globalization;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/arcana-gl"), ApiController]
    [AllowAnonymous]
    public class ArcanaGL : ControllerBase
    {
        private readonly IMediator _mediator;
        public ArcanaGL(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiKeyAuth]
        public async Task<IActionResult> Get([FromQuery] ArcanaGLQuery query)
        {
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result);
            }

        }

        public class ArcanaGLQuery : IRequest<Result<List<ArcanaGLResult>>>
        {
            public string adjustment_month { get; set; }
        }

        public class ArcanaGLResult
        {
            public string SyncId { get; set; }
            public string Mark1 { get; set; }
            public string Mark2 { get; set; }
            public string AssetCIP { get; set; }
            public string AccountingTag { get; set; }
            public string TransactionDate { get; set; }
            public string ClientSupplier { get; set; }
            public string AccountTitleCode { get; set; }
            public string AccountTitle { get; set; }
            public string CompanyCode { get; set; }
            public string Company { get; set; }
            public string DivisionCode { get; set; }
            public string Division { get; set; }
            public string DepartmentCode { get; set; }
            public string Department { get; set; }
            public string UnitCode { get; set; }
            public string Unit { get; set; }
            public string SubUnitCode { get; set; }
            public string SubUnit { get; set; }
            public string LocationCode { get; set; }
            public string Location { get; set; }
            public string PONumber { get; set; }
            public string RRNumber { get; set; }
            public string ReferenceNo { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal? Quantity { get; set; }
            public string UOM { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal? LineAmount { get; set; }
            public string VoucherJournal { get; set; }
            public string AccountType { get; set; }
            public string DRCR { get; set; }
            public string AssetCode { get; set; }
            public string Asset { get; set; }
            public string ServiceProviderCode { get; set; }
            public string ServiceProvider { get; set; }
            public string BOA { get; set; }
            public string Allocation { get; set; }
            public string AccountGroup { get; set; }
            public string AccountSubGroup { get; set; }
            public string FinancialStatement { get; set; }
            public string UnitResponsible { get; set; }
            public string Batch { get; set; }
            public string Remarks { get; set; }
            public string PayrollPeriod { get; set; }
            public string Position { get; set; }
            public string PayrollType { get; set; }
            public string PayrollType2 { get; set; }
            public string DepreciationDescription { get; set; }
            public string RemainingDepreciationValue { get; set; }
            public string UsefulLife { get; set; }
            public string Month { get; set; }
            public string Year { get; set; }
            public string Particulars { get; set; }
            public string Month2 { get; set; }
            public string FarmType { get; set; }
            public string JeanRemarks { get; set; }
            public string From { get; set; }
            public string ChangeTo { get; set; }
            public string Reason { get; set; }
            public string CheckingRemarks { get; set; }
            public string BankName { get; set; }
            public string ChequeNumber { get; set; }
            public string ChequeVoucherNumber { get; set; }
            public string BOA2 { get; set; }
            public string System { get; set; }
            public string Books { get; set; }
        }

        public class Handler : IRequestHandler<ArcanaGLQuery, Result<List<ArcanaGLResult>>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result<List<ArcanaGLResult>>> Handle(ArcanaGLQuery request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.adjustment_month))
                {
                    return Result.Success(new List<ArcanaGLResult>());
                }

                if (!DateTime.TryParseExact(request.adjustment_month, "yyyy-MM",
                                            CultureInfo.InvariantCulture, DateTimeStyles.None,
                                            out DateTime adjustmentMonth))
                {
                    throw new ArgumentException("Adjustment_month must be in the format yyyy-MM");
                }

                var startDate = new DateTime(adjustmentMonth.Year, adjustmentMonth.Month, 1);
                var endDate = startDate.AddMonths(1);

                var transactions = await _context.Transactions
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(t => t.Client)
                        .ThenInclude(c => c.Cluster)
                    .Include(t => t.TransactionSales)
                    .Include(t => t.TransactionItems)
                        .ThenInclude(i => i.Item)
                            .ThenInclude(it => it.Uom)
                    .Include(t => t.PaymentTransactions)
                    .Where(t => t.CreatedAt >= startDate && t.CreatedAt < endDate)
                    .ToListAsync(cancellationToken);

                var result = transactions.SelectMany(t =>
                    t.TransactionItems.SelectMany(ti => new List<ArcanaGLResult>
                    {

                    //credit
                    new ArcanaGLResult
                    {
                        SyncId = "A" + (ti?.Id.ToString() ?? string.Empty),
                        Mark1 = "SJ",
                        Mark2 = string.Empty,
                        AssetCIP = string.Empty,
                        AccountingTag = t?.InvoiceType == "Charge"
                            ? $"CI#{t?.InvoiceNo ?? string.Empty}"
                            : t?.InvoiceType == "Sales"
                                ? $"SI#{t?.InvoiceNo ?? string.Empty}"
                                : t?.InvoiceNo ?? string.Empty,
                        TransactionDate = t?.CreatedAt.ToString("yyyy-MM-dd") ?? string.Empty,
                        ClientSupplier = t?.Client?.BusinessName ?? string.Empty,
                        AccountTitleCode = "411200",
                        AccountTitle = "Account Receivable",
                        CompanyCode = "0001",
                        Company = "RDFFLFI",
                        DivisionCode = "31",
                        Division = "Fresh Options",
                        DepartmentCode = "7200",
                        Department = "General Trade Distributorship",
                        UnitCode = string.Empty,
                        Unit = string.Empty,
                        SubUnitCode = string.Empty,
                        SubUnit = string.Empty,
                        LocationCode = "1679",
                        Location = t?.Client?.Cluster?.ClusterType ?? string.Empty,
                        PONumber = string.Empty,
                        RRNumber = string.Empty,
                        ReferenceNo = t?.InvoiceType == "Charge"
                            ? $"CI#{t?.InvoiceNo ?? string.Empty}"
                            : t?.InvoiceType == "Sales"
                                ? $"SI#{t?.InvoiceNo ?? string.Empty}"
                                : t?.InvoiceNo ?? string.Empty,
                        ItemCode = ti?.Item?.ItemCode ?? string.Empty,
                        ItemDescription = ti?.Item?.ItemDescription ?? string.Empty,
                        Quantity = ti?.Quantity ?? 0,
                        UOM = ti?.Item?.Uom?.UomDescription ?? string.Empty,
                        UnitPrice = ti?.UnitPrice ?? 0,
                        LineAmount = -(ti?.Amount ?? 0),
                        VoucherJournal = string.Empty,
                        AccountType = "INCOME",
                        DRCR = "Credit",
                        AssetCode = string.Empty,
                        Asset = string.Empty,
                        ServiceProviderCode = string.Empty,
                        ServiceProvider = string.Empty,
                        BOA = "Inventoriable",
                        Allocation = "0",
                        AccountGroup = string.Empty,
                        AccountSubGroup = string.Empty,
                        FinancialStatement = "Income Statement",
                        UnitResponsible = "MAU",
                        Batch = string.Empty,
                        Remarks = string.Empty,
                        PayrollPeriod = string.Empty,
                        Position = string.Empty,
                        PayrollType = string.Empty,
                        PayrollType2 = string.Empty,
                        DepreciationDescription = string.Empty,
                        RemainingDepreciationValue = string.Empty,
                        UsefulLife = string.Empty,
                        Month = t?.CreatedAt.ToString("MMM") ?? string.Empty,
                        Year = t?.CreatedAt.ToString("yyyy") ?? string.Empty,
                        Particulars = string.Empty,
                        Month2 = string.Empty,
                        FarmType = string.Empty,
                        JeanRemarks = string.Empty,
                        From = string.Empty,
                        ChangeTo = string.Empty,
                        Reason = string.Empty,
                        CheckingRemarks = string.Empty,
                        BankName = string.Empty,
                        ChequeNumber = string.Empty,
                        ChequeVoucherNumber = string.Empty,
                        BOA2 = "Inventoriable",
                        System = "Arcana",
                        Books = "Journal Book"
                    },



                    //debit
                    new ArcanaGLResult
                    {
                        SyncId = "A" + (ti?.Id.ToString() ?? string.Empty),
                        Mark1 = "SJ",
                        Mark2 = string.Empty,
                        AssetCIP = string.Empty,
                        AccountingTag = t?.InvoiceType == "Charge"
                            ? $"CI#{t?.InvoiceNo ?? string.Empty}"
                            : t?.InvoiceType == "Sales"
                                ? $"SI#{t?.InvoiceNo ?? string.Empty}"
                                : t?.InvoiceNo ?? string.Empty,
                        TransactionDate = t?.CreatedAt.ToString("yyyy-MM-dd") ?? string.Empty,
                        ClientSupplier = t?.Client?.BusinessName ?? string.Empty,
                        AccountTitleCode = "411200",
                        AccountTitle = t.Status == "Pending"
                            ? "Sales Commercial"
                            : "Cash on Hand",
                        CompanyCode = "0001",
                        Company = "RDFFLFI",
                        DivisionCode = "31",
                        Division = "Fresh Options",
                        DepartmentCode = "7200",
                        Department = "General Trade Distributorship",
                        UnitCode = string.Empty,
                        Unit = string.Empty,
                        SubUnitCode = string.Empty,
                        SubUnit = string.Empty,
                        LocationCode = "1679",
                        Location = t?.Client?.Cluster?.ClusterType ?? string.Empty,
                        PONumber = string.Empty,
                        RRNumber = string.Empty,
                        ReferenceNo = t?.InvoiceType == "Charge"
                            ? $"CI#{t?.InvoiceNo ?? string.Empty}"
                            : t?.InvoiceType == "Sales"
                                ? $"SI#{t?.InvoiceNo ?? string.Empty}"
                                : t?.InvoiceNo ?? string.Empty,
                        ItemCode = ti?.Item?.ItemCode ?? string.Empty,
                        ItemDescription = ti?.Item?.ItemDescription ?? string.Empty,
                        Quantity = ti?.Quantity ?? 0,
                        UOM = ti?.Item?.Uom?.UomDescription ?? string.Empty,
                        UnitPrice = ti?.UnitPrice ?? 0, 
                        LineAmount = ti?.Amount ?? 0,      
                        VoucherJournal = string.Empty,
                        AccountType = "INCOME",
                        DRCR = "Debit",
                        AssetCode = string.Empty,
                        Asset = string.Empty,
                        ServiceProviderCode = string.Empty,
                        ServiceProvider = string.Empty,
                        BOA = "Inventoriable",
                        Allocation = "0",
                        AccountGroup = string.Empty,
                        AccountSubGroup = string.Empty,
                        FinancialStatement = "Income Statement",
                        UnitResponsible = "MAU",
                        Batch = string.Empty,
                        Remarks = string.Empty,
                        PayrollPeriod = string.Empty,
                        Position = string.Empty,
                        PayrollType = string.Empty,
                        PayrollType2 = string.Empty,
                        DepreciationDescription = string.Empty,
                        RemainingDepreciationValue = string.Empty,
                        UsefulLife = string.Empty,
                        Month = t?.CreatedAt.ToString("MMM") ?? string.Empty,
                        Year = t?.CreatedAt.ToString("yyyy") ?? string.Empty,
                        Particulars = string.Empty,
                        Month2 = string.Empty,
                        FarmType = string.Empty,
                        JeanRemarks = string.Empty,
                        From = string.Empty,
                        ChangeTo = string.Empty,
                        Reason = string.Empty,
                        CheckingRemarks = string.Empty,
                        BankName = string.Empty,
                        ChequeNumber = string.Empty,
                        ChequeVoucherNumber = string.Empty,
                        BOA2 = "Inventoriable",
                        System = "Arcana",
                        Books = "Journal Book"
                    }
                    })
                     ).ToList();


                return Result.Success(result);
            }
        }
    }
}
