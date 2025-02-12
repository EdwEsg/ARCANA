
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Globalization;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/arcana-gl"), ApiController]
    public class ArcanaGL : ControllerBase
    {
        private readonly IMediator _mediator;
        public ArcanaGL(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ArcanaGLQuery query)
        {
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(new { custom_header = result.Value });
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
            public string Mark { get; set; }
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
            public string Quantity { get; set; }
            public string UOM { get; set; }
            public string UnitPrice { get; set; }
            public string LineAmount { get; set; }
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
                if (!DateTime.TryParseExact(request.adjustment_month, "yyyy-MM",
                                            CultureInfo.InvariantCulture, DateTimeStyles.None,
                                            out DateTime adjustmentMonth))
                {
                    throw new ArgumentException("Adjustment_month must be in the format yyyy-MM");
                }

                var startDate = new DateTime(adjustmentMonth.Year, adjustmentMonth.Month, 1);
                var endDate = startDate.AddMonths(1);

                var transactions = await _context.Transactions
                    .Include(t => t.TransactionItems)
                    .Where(t => t.CreatedAt >= startDate && t.CreatedAt < endDate)
                    .ToListAsync(cancellationToken);

                var result = transactions.SelectMany(t =>
                    t.TransactionItems.Select(ti => new ArcanaGLResult
                    {
                        SyncId = string.Empty,
                        Mark = string.Empty,
                        Mark2 = string.Empty,
                        AssetCIP = string.Empty,
                        AccountingTag = string.Empty,
                        TransactionDate = t.CreatedAt.ToString("yyyy-MM-dd"),
                        ClientSupplier = string.Empty,
                        AccountTitleCode = string.Empty,
                        AccountTitle = string.Empty,
                        CompanyCode = string.Empty,
                        Company = string.Empty,
                        DivisionCode = string.Empty,
                        Division = string.Empty,
                        DepartmentCode = string.Empty,
                        Department = string.Empty,
                        UnitCode = string.Empty,
                        Unit = string.Empty,
                        SubUnitCode = string.Empty,
                        SubUnit = string.Empty,
                        LocationCode = string.Empty,
                        Location = string.Empty,
                        PONumber = string.Empty,
                        RRNumber = string.Empty,
                        ReferenceNo = string.Empty,
                        ItemCode = string.Empty,
                        ItemDescription = string.Empty,
                        Quantity = ti.Quantity.ToString(),
                        UOM = string.Empty,
                        UnitPrice = ti.UnitPrice.ToString(),
                        LineAmount = ti.Amount.ToString(),
                        VoucherJournal = string.Empty,
                        AccountType = string.Empty,
                        DRCR = string.Empty,
                        AssetCode = string.Empty,
                        Asset = string.Empty,
                        ServiceProviderCode = string.Empty,
                        ServiceProvider = string.Empty,
                        BOA = string.Empty,
                        Allocation = string.Empty,
                        AccountGroup = string.Empty,
                        AccountSubGroup = string.Empty,
                        FinancialStatement = string.Empty,
                        UnitResponsible = string.Empty,
                        Batch = string.Empty,
                        Remarks = string.Empty,
                        PayrollPeriod = string.Empty,
                        Position = string.Empty,
                        PayrollType = string.Empty,
                        PayrollType2 = string.Empty,
                        DepreciationDescription = string.Empty,
                        RemainingDepreciationValue = string.Empty,
                        UsefulLife = string.Empty,
                        Month = string.Empty,
                        Year = string.Empty,
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
                        BOA2 = string.Empty,
                        System = string.Empty,
                        Books = string.Empty
                    })).ToList();

                return Result.Success(result);
            }
        }
    }
}
