//using ClosedXML.Excel;
//using Microsoft.AspNetCore.Mvc;
//using RDF.Arcana.API.Data;


//namespace RDF.Arcana.API.Features.Reports;

//public class CDOReports
//{
//    public class CDOReportsCommand : IRequest<IActionResult>
//    {
//        public DateTime DateFrom { get; set; }
//        public DateTime DateTo { get; set; }
//    }

//    public class Handler : IRequestHandler<CDOReportsCommand, IActionResult>
//    {
//        private readonly ArcanaDbContext _context;
//        public Handler(ArcanaDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Handle(CDOReportsCommand request, CancellationToken cancellationToken)
//        {
//            var query = _context.TransactionItems
//                .Include(t => t.Transaction)
//                    .ThenInclude(ts => ts.TransactionSales)
//                .Include(i => i.Item)
//                .Where(ti => ti.CreatedAt >= request.DateFrom && ti.CreatedAt <= request.DateTo)
//                .AsSplitQuery()
//                .AsNoTracking();

//            var consolidate = await query.ToListAsync(cancellationToken);

//            using (var workbook = new XLWorkbook())
//            {
//                var worksheet = workbook.Worksheets.Add("CDO Reports");

//                var headers = new List<string>
//                {
//                    "Remarks (Old/New)",
//                    "Month",
//                    "Date",
//                    "Outlet Code",
//                    "SI#",
//                    "Item Code",
//                    "Quantity",
//                    "Discount",
//                    "Price",
//                    "Amount",
//                    "Discounted Price",
//                    "Discounted Amount",
//                    "Item Description",
//                    "Outlet",
//                    "Municipality",
//                    "Province",
//                    "Area",
//                    "New Cluster",
//                    "Customer Type",
//                    "Channel",
//                    "Status",
//                    "Region"
//                };

//                var headerRange = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, headers.Count));
//                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#544d91");
//                headerRange.Style.Font.Bold = true;
//                headerRange.Style.Font.FontColor = XLColor.White;
//                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
//                headerRange.Style.Border.OutsideBorderColor = XLColor.Black;
//                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

//                for (var index = 0; index < headers.Count; index++)
//                {
//                    worksheet.Cell(1, index + 1).Value = headers[index];
//                }

//                var evenRowColor = XLColor.FromHtml("#eae9f4");
//                var oddRowColor = XLColor.FromHtml("#dcd9e9");

//                for (var index = 0; index < consolidate.Count; index++)
//                {
//                    var row = worksheet.Row(index + 2);

//                    var rowColor = index % 2 == 0 ? evenRowColor : oddRowColor;
//                    row.Style.Fill.BackgroundColor = rowColor;

//                    row.Cell(1).Value = "N/A";
//                    row.Cell(2).Value = consolidate[index].CreatedAt.ToString("MMM");
//                    row.Cell(3).Value = consolidate[index].CreatedAt.ToString("dd/MM/yyyy");
//                    row.Cell(4).Value = "N/A";
//                    row.Cell(5).Value = consolidate[index].Transaction.InvoiceNo;
//                    row.Cell(6).Value = consolidate[index].Item.ItemCode;
//                    row.Cell(7).Value = consolidate[index].Quantity;
//                    row.Cell(8).Value = (consolidate[index].Transaction.TransactionSales.Discount * 100).ToString("0.##") + "%";
//                    row.Cell(9).Value = consolidate[index].UnitPrice;
//                    row.Cell(10).Value = consolidate[index].Amount;
//                    row.Cell(11).Value = consolidate[index].    

//                }
//            }

//        }
//    }
//}
