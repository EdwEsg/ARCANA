using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/AS-reports"), ApiController]
    public class ASReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public ASReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ASReport([FromQuery] ASReportsCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AddedBy = userId;
                }

                var result = await _mediator.Send(command);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class ASReportsCommand : IRequest<IActionResult>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }

        public class Handler : IRequestHandler<ASReportsCommand, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            // Helper method to convert column numbers to Excel column letters
            private static string GetExcelColumnName(int columnNumber)
            {
                int dividend = columnNumber;
                string columnName = String.Empty;
                int modulo;
                while (dividend > 0)
                {
                    modulo = (dividend - 1) % 26;
                    columnName = Convert.ToChar(65 + modulo) + columnName;
                    dividend = (int)((dividend - modulo) / 26);
                }
                return columnName;
            }


            public async Task<IActionResult> Handle(ASReportsCommand request, CancellationToken cancellationToken)
            {
                var query = _context.Transactions
                    .Include(ts => ts.TransactionSales)
                    .Include(c => c.Client)
                        .ThenInclude(cl => cl.Cluster)
                            .ThenInclude(cdo => cdo.CdoClusters)
                    .Include(c => c.Client)
                        .ThenInclude(t => t.Term)
                            .ThenInclude(td => td.TermDays)
                    .Include(pt => pt.PaymentTransactions)
                        .ThenInclude(pr => pr.PaymentRecord)
                    .Include(pt => pt.PaymentTransactions)
                        .ThenInclude(cl => cl.ClearedPayment)
                    .Include(u => u.AddedByUser)
                    .AsSplitQuery()
                    .AsNoTracking();

                var currentUserRole = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == request.AddedBy, cancellationToken);

                //acess only for admin and finance 
                if (request.AddedBy == 1 || currentUserRole.UserRolesId == 9)
                {
                    query = query.Where(t => t.CreatedAt >= request.DateFrom && t.CreatedAt <= request.DateTo);
                }
                else
                {
                    return new UnauthorizedResult();
                }

                var consolidate = await query.ToListAsync(cancellationToken);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("AS Reports");

                    var headers = new List<string>
                    {
                        "Date",
                        "Month",
                        "CI",
                        "SI",
                        "Business",
                        "Customer",
                        "Gross Sales",
                        "Net Sales",
                        "Atoe",
                        "Tax W/Held",
                        "Sales Return",
                        "Listing Fee",
                        "Expenses",
                        "Discount",
                        "Write Off",
                        "Misc. Income",
                        "Offset",
                        "Returned Checks",
                        "Payment",
                        "AR",
                        "Remaining Balance w/ PDC",
                        "Paid?",
                        "OR",
                        "AR No.",
                        "Tag",
                        "Check",
                        "Check Date",
                        "Check Month",
                        "Date Remit",
                        "OR Date",
                        "OR Month",
                        "Collected",
                        "Received 2307/ expenses",
                        "Remarks",
                        "Aging",
                        "Store",
                        "Sales Rep",
                        "Manager",
                        "Status",
                        "Area",
                        "SAS",
                        "Terms",
                        "RSH",
                        "Category",
                        "Previous Sales Rep",
                    };

                    var headerRange = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, headers.Count));
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#544d91");
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Font.FontColor = XLColor.White;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    headerRange.Style.Border.OutsideBorderColor = XLColor.Black;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    for (var index = 0; index < headers.Count; index++)
                    {
                        worksheet.Cell(1, index + 1).Value = headers[index];
                    }

                    var evenRowColor = XLColor.FromHtml("#eae9f4");
                    var oddRowColor = XLColor.FromHtml("#dcd9e9");

                    for (var index = 0; index < consolidate.Count; index++)
                    {
                        var row = worksheet.Row(index + 2);

                        var rowColor = index % 2 == 0 ? evenRowColor : oddRowColor;
                        row.Style.Fill.BackgroundColor = rowColor;

                        int rowNumber = index + 2;

                        row.Cell(1).Value = consolidate[index].CreatedAt;  
                        row.Cell(1).Style.DateFormat.Format = "MM/dd/yy"; //Date
                        row.Cell(2).Value = consolidate[index].CreatedAt.ToString("yyyyMM"); //Month
                        row.Cell(3).Value = consolidate[index].InvoiceType == "Charge" ? consolidate[index].InvoiceNo : ""; //CI
                        row.Cell(4).Value = consolidate[index].InvoiceType == "Sales" ? consolidate[index].InvoiceNo : ""; //SI
                        row.Cell(5).Value = consolidate[index].Client.BusinessName; //Business
                        row.Cell(6).Value = consolidate[index].Client.Fullname; //Customer
                        row.Cell(7).Value = consolidate[index].TransactionSales.SubTotal; //Gross Sales
                        row.Cell(8).Value = consolidate[index].TransactionSales.TotalAmountDue; //Net Sales
                        row.Cell(9).Value = 0; //Atoe

                        var withholding = consolidate[index].PaymentTransactions.FirstOrDefault(pt => pt.PaymentMethod == PaymentMethods.Withholding);
                        row.Cell(10).Value = withholding?.TotalAmountReceived ?? 0; //Tax w/held

                        row.Cell(11).Value = 0; //Sales Return

                        var listing = consolidate[index].PaymentTransactions.FirstOrDefault(pt => pt.PaymentMethod == PaymentMethods.ListingFee);
                        row.Cell(12).Value = listing?.TotalAmountReceived ?? 0; //Listing Fee

                        var expense = consolidate[index].PaymentTransactions.Where(pt => pt.PaymentMethod == PaymentMethods.Others);
                        row.Cell(13).Value = expense?.Sum(e => e.TotalAmountReceived) ?? 0; //Expenses

                        row.Cell(14).Value = consolidate[index].TransactionSales.DiscountAmount; //Discount
                        row.Cell(15).Value = 0; //Write off
                        row.Cell(16).Value = 0; //Misc. Income
                        row.Cell(17).Value = 0; //Offset
                        row.Cell(18).Value = 0; //Returned Checks

                        string grossSalesColumn = GetExcelColumnName(7); // G
                        string discountColumn = GetExcelColumnName(14); // N
                        string paymentFormula = $"={grossSalesColumn}{rowNumber}-{discountColumn}{rowNumber}";
                        row.Cell(19).FormulaA1 = paymentFormula; //Payment

                        string taxWithheldColumn = GetExcelColumnName(10); // J
                        string paymentColumn = GetExcelColumnName(19); // S
                        string atoeColumn = GetExcelColumnName(9); // I
                        string offsetColumn = GetExcelColumnName(17); // Q
                        string writeOffColumn = GetExcelColumnName(15); // O
                        string miscIncomeColumn = GetExcelColumnName(16); // P
                        string salesReturnColumn = GetExcelColumnName(11); // K
                        string expensesColumn = GetExcelColumnName(13); // M
                        string listingFeeColumn = GetExcelColumnName(12); // L
                        string arColumn = GetExcelColumnName(20); // T
                        string arFormula = $"={grossSalesColumn}{rowNumber}-({taxWithheldColumn}{rowNumber}+{paymentColumn}{rowNumber}+{atoeColumn}{rowNumber}+{discountColumn}{rowNumber}+{offsetColumn}{rowNumber}+{writeOffColumn}{rowNumber}+{miscIncomeColumn}{rowNumber}+{salesReturnColumn}{rowNumber}+{expensesColumn}{rowNumber}+{listingFeeColumn}{rowNumber})";
                        row.Cell(20).FormulaA1 = arFormula;//AR

                        string paidColumn = GetExcelColumnName(22); // V
                        string remainingBalanceFormula = $"=IF({paidColumn}{rowNumber}=\"Y\",{paymentColumn}{rowNumber}+{arColumn}{rowNumber},IF({grossSalesColumn}{rowNumber}=({paymentColumn}{rowNumber}-{atoeColumn}{rowNumber}-{taxWithheldColumn}{rowNumber}-{salesReturnColumn}{rowNumber}-{expensesColumn}{rowNumber}-{discountColumn}{rowNumber}-{writeOffColumn}{rowNumber}-{miscIncomeColumn}{rowNumber}-{offsetColumn}{rowNumber}-{listingFeeColumn}{rowNumber}),\"\",{arColumn}{rowNumber}))";
                        row.Cell(21).FormulaA1 = remainingBalanceFormula; //Remaiining Balance w/ PDC

                        row.Cell(22).Value = consolidate[index].TransactionSales.RemainingBalance == 0 ? "Y" : ""; //Paid
                        row.Cell(23).Value = ""; //OR
                        row.Cell(24).Value = consolidate[index]?.PaymentTransactions.FirstOrDefault()?.PaymentRecord?.ReceiptNo ?? ""; //AR No.
                        row.Cell(25).Value = consolidate[index]?.PaymentTransactions.FirstOrDefault()?.ClearedPayment?.ATag ?? ""; //Tag
                        row.Cell(26).Value = consolidate[index]?.PaymentTransactions.FirstOrDefault()?.PaymentMethod ?? ""; //Check

                        var check = consolidate[index].PaymentTransactions.Where(pm => pm.PaymentMethod == PaymentMethods.Cheque);
                        row.Cell(27).Value = check.FirstOrDefault()?.ChequeDate.ToString("MM/dd/yy") ?? ""; //Check Date
                        row.Cell(28).Value = check.FirstOrDefault()?.ChequeDate.ToString("yyyyMM") ?? ""; //Check Month

                        row.Cell(29).Value = consolidate[index].PaymentTransactions.FirstOrDefault()?.ClearedPayment?.CreatedAt.ToString("MM/dd/yy") ?? ""; //Date Remit
                        row.Cell(30).Value = ""; //OR Date
                        row.Cell(31).Value = ""; //OR Month
                        row.Cell(32).Value = ""; //Collected

                        var withholdingOthers = consolidate[index].PaymentTransactions.FirstOrDefault(pm => pm.PaymentMethod == PaymentMethods.Withholding ||
                            pm.PaymentMethod == PaymentMethods.Others);
                        row.Cell(33).Value = withholdingOthers?.DateReceived.ToString("MM/dd/yy") ?? ""; //Received 2307/ expenses;
                        row.Cell(34).Value = ""; //Remarks

                        string dateColumn = GetExcelColumnName(1); // A
                        string agingFormula = $"=INT(TODAY()-{dateColumn}{rowNumber})";
                        row.Cell(35).FormulaA1 = agingFormula; // Aging

                        row.Cell(36).Value = ""; //Store
                        row.Cell(37).Value = ""; //Sales Rep
                        row.Cell(38).Value = ""; //Manager
                        row.Cell(39).Value = consolidate[index].Client.IsActive == true ? "Active" : ""; //Status
                        row.Cell(40).Value = consolidate[index].Client.Cluster.ClusterType; //Area


                        row.Cell(41).Value = consolidate[index].AddedByUser.Fullname; //SAS

                        row.Cell(42).Value = consolidate[index]?.Client?.Term?.TermDays?.Days ?? 0; //Terms
                        row.Cell(43).Value = ""; //RSH
                        row.Cell(44).Value = "Gen Trade"; //Category
                        row.Cell(45).Value = ""; //Previous Sales Rep

                        //for centering the numeric value for better readability
                        for (int col = 1; col <= 45; col++)
                        {
                            var cell = row.Cell(col);
                            if (decimal.TryParse(cell.Value.ToString(), out _))
                            {
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }
                        }
                    }

                    worksheet.Columns().AdjustToContents();

                    var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    string fileName = $"AS_Reports_{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };

                }
            }
        }
    }
}
