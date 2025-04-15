using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/payment-reports"), ApiController]
    public class PaymentReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaymentReportsQuery query)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    query.AddedBy = userId;
                }

                var result = await _mediator.Send(query);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class PaymentReportsQuery : IRequest<IActionResult>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class Handler : IRequestHandler<PaymentReportsQuery, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(PaymentReportsQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var query = _context.PaymentTransactions
                    .Include(x => x.AddedByUser)
                    .Include(t => t.Transaction)
                        .ThenInclude(c => c.Client)
                    .Where(ti =>
                        ti.DateReceived >= request.DateFrom &&
                        ti.DateReceived < adjustedDateTo &&
                        ti.Transaction.Status != Status.Voided)
                    .AsSplitQuery()
                    .AsNoTracking();

                query = query.Where(ti => ti.DateReceived >= request.DateFrom && ti.DateReceived < adjustedDateTo);

                if (request.ClusterId != null)
                {
                    query = query
                        .Where(p => p.Transaction.Client.ClusterId == request.ClusterId);
                }

                var consolidate = await query.ToListAsync(cancellationToken);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Void Transaction Reports");

                    var headers = new List<string>
                {
                    "Date",
                    "Transaction Id",
                    "Payment Method",
                    "Payment Amount",
                    "Payee",
                    "Added By",
                    "Reason",
                    "Status",
                    "ReferenceNo"
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

                        row.Cell(1).Value = consolidate[index].DateReceived;
                        row.Cell(2).Value = consolidate[index].TransactionId;
                        row.Cell(3).Value = consolidate[index].PaymentMethod;
                        row.Cell(4).Value = consolidate[index].PaymentAmount;
                        row.Cell(5).Value = consolidate[index].Payee;
                        row.Cell(6).Value = consolidate[index].AddedByUser.Fullname;
                        row.Cell(7).Value = consolidate[index].Reason;
                        row.Cell(8).Value = consolidate[index].Status;
                        row.Cell(9).Value = consolidate[index].ReferenceNo;


                        //for centering the numeric value for better readability
                        for (int col = 1; col <= 10; col++)
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

                    string fileName = $"Payment_Reports_{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }
    }
}
