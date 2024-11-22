using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Get_Reports;
using static RDF.Arcana.API.Features.Listing_Fee.GetListingFeeHistoryByClientId.GetListingFeeHistoryByClientIdResult;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/listing-fee-reports"), ApiController]
    public class ListingFeeReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public ListingFeeReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetListingFeeReport([FromQuery] ListingFeeCommand query)
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

        public class ListingFeeCommand : IRequest<IActionResult>
        {
            public int ClientId { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }

        public class Handler : IRequestHandler<ListingFeeCommand, IActionResult>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(ListingFeeCommand request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var client = await _context.Clients
                    .Where(c => c.Id == request.ClientId)
                    .Select(c => c.BusinessName)
                    .FirstOrDefaultAsync(cancellationToken);

                if (client == null)
                {
                    return new NotFoundResult();
                }

                var paymentTransactions = await _context.PaymentTransactions
                    .Where(pt => pt.Transaction.ClientId == request.ClientId &&
                                 pt.PaymentMethod == PaymentMethods.ListingFee &&
                                 pt.Status != Status.Voided &&
                                 pt.Status != Status.Cancelled &&
                                 pt.TotalAmountReceived > 0 &&
                                 pt.DateReceived >= request.DateFrom && pt.DateReceived < adjustedDateTo)
                    .Select(pt => new LHistory
                    {
                        PaymentType = "Payment",
                        Amount = pt.TotalAmountReceived,
                        CreatedDate = pt.DateReceived,
                        TransactedBy = pt.AddedByUser.Fullname
                    })
                    .ToListAsync(cancellationToken);


                var cheques = await _context.Cheque
                    .Where(c => c.ClientId == request.ClientId &&
                                c.Amount > 0 &&
                                c.CreatedDate >= request.DateFrom && c.CreatedDate < adjustedDateTo)
                    .Select(c => new LHistory
                    {
                        PaymentType = "Cheque",
                        Amount = c.Amount,
                        CreatedDate = c.CreatedDate,
                        TransactedBy = c.AddedByUser.Fullname
                    })
                    .ToListAsync(cancellationToken);


                var listingFees = await _context.ListingFees
                    .Where(lf => lf.ClientId == request.ClientId &&
                                 lf.Status == Status.Approved &&
                                 lf.OriginalTotal > 0 &&
                                 lf.CratedAt >= request.DateFrom && lf.CratedAt < adjustedDateTo)
                    .Select(lf => new LHistory
                    {
                        PaymentType = "Add Balance",
                        Amount = lf.OriginalTotal,
                        CreatedDate = lf.CratedAt,
                        TransactedBy = lf.RequestedByUser.Fullname
                    })
                    .ToListAsync(cancellationToken);


                var listingHistory = paymentTransactions
                    .Concat(cheques)
                    .Concat(listingFees)
                    .OrderByDescending(l => l.CreatedDate)
                    .ToList();


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Listing Fee Reports");

                    var headers = new List<string>
                    {
                        "Payment Type",
                        "Amount",
                        "Created Date",
                        "Transacted By"
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

                    //var evenRowColor = XLColor.FromHtml("#eae9f4");
                    //var oddRowColor = XLColor.FromHtml("#dcd9e9");

                    for (var index = 0; index < listingHistory.Count; index++)
                    {
                        //var row = worksheet.Row(index + 2);

                        //var rowColor = index % 2 == 0 ? evenRowColor : oddRowColor;
                        //row.Style.Fill.BackgroundColor = rowColor;

                        var row = worksheet.Row(index + 2);

                        row.Cell(1).Value = listingHistory[index].PaymentType;
                        row.Cell(2).Value = listingHistory[index].Amount;
                        row.Cell(3).Value = listingHistory[index].CreatedDate.ToString("MM/dd/yyyy");
                        row.Cell(4).Value = listingHistory[index].TransactedBy;


                        for (int col = 1; col <= headers.Count; col++)
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

                    string fileName = $"{client}_Listing_Fee_Reports.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        
    
        }

        public class LHistory
        {
            public string PaymentType { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreatedDate { get; set; }
            public string TransactedBy { get; set; }
        }
    }
}
