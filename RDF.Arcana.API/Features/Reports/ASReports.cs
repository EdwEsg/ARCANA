//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Spreadsheet;
//using Microsoft.AspNetCore.Mvc;
//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;

//namespace RDF.Arcana.API.Features.Reports
//{
//    public class ASReports
//    {
//        public class ASReportsCommand : IRequest<IActionResult>
//        {
//            public int? AddedBy { get; set; }
//            public DateTime DateFrom { get; set; }
//            public DateTime DateTo { get; set; }
//        }

//        public class Handler : IRequestHandler<ASReportsCommand, IActionResult>
//        {
//            private readonly ArcanaDbContext _context;
//            public Handler(ArcanaDbContext context)
//            {
//                _context = context;
//            }

//            public async Task<IActionResult> Handle(ASReportsCommand request, CancellationToken cancellationToken)
//            {
//                var query = _context.Transactions
//                    .Include(ts => ts.TransactionSales)
//                    .Include(c => c.Client)
//                    .Include(pt => pt.PaymentTransactions)
//                    .AsSplitQuery()
//                    .AsNoTracking();

//                var currentUserRole = await _context.Users
//                    .AsNoTracking()
//                    .FirstOrDefaultAsync(u => u.Id == request.AddedBy, cancellationToken);

//                //acess only for admin and finance 
//                if (request.AddedBy == 1 || currentUserRole.UserRolesId == 9)
//                {
//                    query = query.Where(t => t.CreatedAt >= request.DateFrom && t.CreatedAt <= request.DateTo);
//                }
//                else
//                {
//                    return new UnauthorizedResult();
//                }

//                var consolidate = await query.ToListAsync(cancellationToken);

//                using (var workbook = new XLWorkbook())
//                {
//                    var worksheet = workbook.Worksheets.Add("AS Reports");

//                    var headers = new List<string>
//                    {
//                        "Date",
//                        "Month",
//                        "CI",
//                        "SI",
//                        "Business",
//                        "Customer",
//                        "Gross Sales",
//                        "Net Sales",
//                        "Atoe",
//                        "Tax W/Held",
//                        "Sales Return",
//                        "Listing Fee",
//                        "Expenses",
//                        "Discount",
//                        "Write Off",
//                        "Misc. Income",
//                        "Offset",
//                        "Returned Checks",
//                        "Payment",
//                        "AR",
//                        "Remaining Balance w/ PDC",
//                        "Paid?",
//                        "OR",
//                        "AR",
//                        "Tag",
//                        "Check",
//                        "Check Date",
//                        "Check Month",
//                        "Date Remit",
//                        "OR Date",
//                        "OR Month",
//                        "Collected",
//                        "Received 2307/ expenses",
//                        "Remarks",
//                        "Aging",
//                        "Store",
//                        "Sales Rep",
//                        "Manager",
//                        "Statuss",
//                        "Area",
//                        "SAS",
//                        "Terms",
//                        "RSH",
//                        "Category",
//                        "Previous Sales Rep",
//                    };

//                    var headerRange = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, headers.Count));
//                    headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#544d91");
//                    headerRange.Style.Font.Bold = true;
//                    headerRange.Style.Font.FontColor = XLColor.White;
//                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
//                    headerRange.Style.Border.OutsideBorderColor = XLColor.Black;
//                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

//                    for (var index = 0; index < headers.Count; index++)
//                    {
//                        worksheet.Cell(1, index + 1).Value = headers[index];
//                    }

//                    var evenRowColor = XLColor.FromHtml("#eae9f4");
//                    var oddRowColor = XLColor.FromHtml("#dcd9e9");

//                    for (var index = 0; index < consolidate.Count; index++)
//                    {
//                        var row = worksheet.Row(index + 2);

//                        var rowColor = index % 2 == 0 ? evenRowColor : oddRowColor;
//                        row.Style.Fill.BackgroundColor = rowColor;

//                        row.Cell(1).Value = consolidate[index].CreatedAt.ToString("MM/dd/yy");
//                        row.Cell(2).Value = consolidate[index].CreatedAt.ToString("yyyyMM");
//                        row.Cell(3).Value = consolidate[index].InvoiceType == "Charge" ? consolidate[index].InvoiceNo : "";
//                        row.Cell(4).Value = consolidate[index].InvoiceType == "Sales" ? consolidate[index].InvoiceNo : "";
//                        row.Cell(5).Value = consolidate[index].Client.BusinessName;
//                        row.Cell(6).Value = consolidate[index].Client.Fullname;
//                        row.Cell(7).Value = consolidate[index].TransactionSales.SubTotal;
//                        row.Cell(8).Value = consolidate[index].TransactionSales.TotalAmountDue;
//                        row.Cell(9).Value = ""; //ATOE

//                        var withholding = consolidate[index].PaymentTransactions.FirstOrDefault(pt => pt.PaymentMethod == PaymentMethods.Withholding);
//                        row.Cell(10).Value = withholding?.TotalAmountReceived ?? 0;

//                        row.Cell(11).Value = ""; //Sales Return

//                        var listing = consolidate[index].PaymentTransactions.FirstOrDefault(pt => pt.PaymentMethod == PaymentMethods.ListingFee);
//                        row.Cell(12).Value = listing?.TotalAmountReceived ?? 0;

//                        var expense = consolidate[index].PaymentTransactions.Where(pt => pt.PaymentMethod == PaymentMethods.Others);
//                        row.Cell(13).Value = expense?.Sum(e => e.TotalAmountReceived) ?? 0;

//                        row.Cell(14).Value = consolidate[index].TransactionSales.DiscountAmount;
//                        row.Cell(15).Value = ""; //Write off
//                        row.Cell(16).Value = ""; //Misc. Income
//                        row.Cell(17).Value = ""; //Offset
//                        row.Cell(18).Value = ""; //Returned Checks
//                        row.Cell(19).Value = "";

 



//                    }
//                }
//            }
//        }
//    }
//}
