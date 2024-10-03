using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/sales-journal-reports"), ApiController]
    public class SalesJournalReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public SalesJournalReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SalesJournalReport([FromQuery] SalesJournalReportsQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class SalesJournalReportsQuery : IRequest<IActionResult>
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }

        public class Handler : IRequestHandler<SalesJournalReportsQuery, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(SalesJournalReportsQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Transactions
                    .Include(ts => ts.TransactionSales)
                    .Include(c => c.Client)
                        .ThenInclude(ba => ba.BusinessAddress)
                    .Where(t => t.CreatedAt >= request.DateFrom && t.CreatedAt <= request.DateTo)
                    .AsSplitQuery()
                    .AsNoTracking();

                var consolidate = await query.ToListAsync(cancellationToken);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sales Journal Reports");

                    var headers = new List<string>
                    {
                        "Date",
                        "Business Name",
                        "Business Address",
                        "Invoice No.",
                        "Vat Reg. No.",
                        "Amount",
                        "Debit",
                        "Credit"
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

                        row.Cell(1).Value = consolidate[index].CreatedAt.ToString("MM/dd/yy");
                        row.Cell(2).Value = consolidate[index].Client.BusinessName;
                        row.Cell(3).Value = $"{consolidate[index].Client.BusinessAddress.City} {consolidate[index].Client.BusinessAddress.Province}";
                        row.Cell(4).Value = consolidate[index].InvoiceNo;
                        row.Cell(5).Value = "'005-312-439-000";
                        row.Cell(6).Value = consolidate[index].TransactionSales.TotalAmountDue;
                        row.Cell(7).Value = consolidate[index].TransactionSales.TotalAmountDue - consolidate[index].TransactionSales.RemainingBalance;
                        row.Cell(8).Value = consolidate[index].TransactionSales.RemainingBalance;

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

                    string fileName = $"SalesJournal_Reports_{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };

                }
            }
        }
    }
}
