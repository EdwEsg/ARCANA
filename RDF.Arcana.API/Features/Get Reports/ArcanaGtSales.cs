using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RDF.Arcana.API.Features.Authenticate.AuthXApi;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/arcana-gt-sales"), ApiController]
    [AllowAnonymous]
    public class ArcanaGtSales : ControllerBase
    {
        private readonly IMediator _mediator;
        public ArcanaGtSales(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiKeyAuth]
        public async Task<IActionResult> Get([FromQuery] ArcanaGtSalesQuery query)
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

        public class ArcanaGtSalesQuery : IRequest<Result<List<ArcanaGtSalesResult>>>
        {
            public string adjustment_month { get; set; }
        }

        public class ArcanaGtSalesResult
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

        public class Handler : IRequestHandler<ArcanaGtSalesQuery, Result<List<ArcanaGtSalesResult>>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result<List<ArcanaGtSalesResult>>> Handle(ArcanaGtSalesQuery request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.adjustment_month))
                {
                    return Result.Success(new List<ArcanaGtSalesResult>());
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
                    .Where(t => t.Status != Status.Voided &&
                        t.CreatedAt >= startDate && t.CreatedAt < endDate)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(t => t.Client)
                        .ThenInclude(c => c.Cluster)
                    .Include(t => t.TransactionSales)
                    .Include(t => t.TransactionItems)
                        .ThenInclude(i => i.Item)
                            .ThenInclude(it => it.Uom)
                    .Include(t => t.PaymentTransactions)
                    .ToListAsync(cancellationToken);

                var result = transactions.SelectMany(t => new List<ArcanaGtSalesResult>
                {
                    new ArcanaGtSalesResult
                    {
                        //debit ar
                        SyncId = "A" + (t?.Id.ToString() ?? string.Empty),
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
                        AccountTitle = t.Status == Status.Pending
                            ? "Account Receivable"
                            : t.PaymentTransactions.FirstOrDefault(p =>
                                    p.PaymentMethod == PaymentMethods.Cash
                                    || p.PaymentMethod == PaymentMethods.Cheque
                                    || p.PaymentMethod == PaymentMethods.Online
                                ) != null
                                ? "Cash On Hand"
                                : t.PaymentTransactions.FirstOrDefault(p =>
                                        p.PaymentMethod == PaymentMethods.ListingFee
                                    ) != null
                                    ? "Leasing"
                                    : t.PaymentTransactions.FirstOrDefault(p =>
                                            p.PaymentMethod == PaymentMethods.AdvancePayment
                                            ) != null
                                            ? "Advances From the Customer"
                                            : t.PaymentTransactions.FirstOrDefault(p =>
                                                    p.PaymentMethod == PaymentMethods.Withholding
                                                    ) != null
                                                    ? "Withholding Tax"
                                                    : t.PaymentTransactions.FirstOrDefault(p =>
                                                        p.PaymentMethod == PaymentMethods.Others
                                                        ) != null
                                                        ? "Other Expense"
                                                        : "Other",
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
                        ItemCode = string.Empty,
                        ItemDescription = string.Empty,
                        Quantity = t.TransactionItems.Sum(q => q.Quantity),
                        UOM = string.Empty,
                        UnitPrice = 0,
                        LineAmount = t.TransactionSales.TotalSales,
                        VoucherJournal = string.Empty,
                        AccountType = "INCOME",
                        DRCR = "Debit",
                        AssetCode = string.Empty,
                        Asset = string.Empty,
                        ServiceProviderCode = string.Empty,
                        ServiceProvider = string.Empty,
                        BOA = "Sales",
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
                        BOA2 = "Sales Commercial",
                        System = "Arcana",
                        Books = "Sales Journal Book"
                    },




                    new ArcanaGtSalesResult
                    {
                        //credit sales
                        SyncId = "A" + (t?.Id.ToString() ?? string.Empty),
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
                        AccountTitle = "Sales",
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
                        ItemCode = string.Empty,
                        ItemDescription = string.Empty,
                        Quantity = t.TransactionItems.Sum(q => q.Quantity),
                        UOM = string.Empty,
                        UnitPrice = 0,
                        LineAmount = -(t.TransactionSales.TotalSales),
                        VoucherJournal = string.Empty,
                        AccountType = "INCOME",
                        DRCR = "Credit",
                        AssetCode = string.Empty,
                        Asset = string.Empty,
                        ServiceProviderCode = string.Empty,
                        ServiceProvider = string.Empty,
                        BOA = "Sales",
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
                        BOA2 = "Sales Commercial",
                        System = "Arcana",
                        Books = "Sales Journal Book"
                    }
                }
                ).ToList();

                return Result.Success(result);
            }
        }
    }
}
