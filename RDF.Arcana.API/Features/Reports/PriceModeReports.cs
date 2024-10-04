using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/price-mode-reports"), ApiController]
    public class PriceModeReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public PriceModeReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PriceModeReport([FromQuery] PriceModeReportsQuery query)
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

        public class PriceModeReportsQuery : IRequest<IActionResult> { }

        public class Handler : IRequestHandler<PriceModeReportsQuery, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(PriceModeReportsQuery request, CancellationToken cancellationToken)
            {
                var query = _context.PriceModeItems
                    .Include(pm => pm.PriceMode)
                    .Include(i => i.Item)
                    .Include(ipm => ipm.ItemPriceChanges)
                    .Include(u => u.AddedByUser)
                    .OrderBy(iid => iid.ItemId)
                    .AsSplitQuery()
                    .AsNoTracking();

                var consolidate = await query.ToListAsync(cancellationToken);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Price Mode Reports");

                    var headers = new List<string>
                    {
                        "Date Added",
                        "Item Code",
                        "Item",
                        "Price Mode Code",
                        "Price Mode Description",
                        "Clear Pack",
                        "Price",
                        "Added By"
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
                        row.Cell(2).Value = consolidate[index].Item.ItemCode;
                        row.Cell(3).Value = consolidate[index].Item.ItemDescription;
                        row.Cell(4).Value = consolidate[index].PriceMode.PriceModeCode;
                        row.Cell(5).Value = consolidate[index].PriceMode.PriceModeDescription;
                        row.Cell(6).Value = consolidate[index].IsClearPack == true ? "Yes" : "No";
                        row.Cell(7).Value = consolidate[index]?.ItemPriceChanges?.FirstOrDefault()?.Price ?? 0;
                        row.Cell(8).Value = consolidate[index].AddedByUser.Fullname;

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

                    string fileName = $"Price_Mode_Reports.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };

                }
            }
        }
    }
}
