using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/check-in-filo-reports"), ApiController]
    public class CheckInFiloReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public CheckInFiloReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CheckFiloInsResult([FromQuery] CheckInFiloQuery query)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
               && IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    query.AddedBy = userId;

                    var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                }

                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class CheckInFiloQuery : IRequest<IActionResult>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class Handler : IRequestHandler<CheckInFiloQuery, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(CheckInFiloQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var query = _context.CheckIns
                    .Include(c => c.Client)
                    .Include(c => c.CreatedBy)
                    .ThenInclude(u => u.UserRoles)
                    .Include(c => c.CreatedBy)
                    .ThenInclude(u => u.CdoCluster)
                    .Where(t => t.CreatedDate >= request.DateFrom && t.CreatedDate < adjustedDateTo)
                    .AsQueryable();

                var userClusters = _context.CdoClusters
                    .FirstOrDefault(x => x.UserId == request.AddedBy);

                // Per CDO viewing
                if (userClusters != null)
                {
                    query = query.Where(c =>
                        c.CreatedBy.UserRoles.Id == 6 &&
                        c.CreatedBy.CdoCluster.ClusterId == userClusters.ClusterId);
                }

                if (request.ClusterId is not null)
                {
                    query = query.Where(c => c.CreatedBy.CdoCluster.ClusterId == request.ClusterId);
                }

                // Consolidate data by User and Date
                var consolidatedData = query
                    .GroupBy(c => new
                    {
                        c.CreatedBy.Fullname,
                        Date = c.CreatedDate.Date
                    })
                    .Select(g => new
                    {
                        User = g.Key.Fullname,
                        TimeIn = g.Min(x => x.CreatedDate),
                        TimeOut = g
                            .Where(x => x.TimeOut.HasValue)
                            .OrderByDescending(x => x.TimeOut)
                            .Select(x => x.TimeOut)
                            .FirstOrDefault(), 
                        Duration = g.Where(x => x.TimeOut.HasValue).Any()
                            ? $"{(g.Where(x => x.TimeOut.HasValue).OrderByDescending(x => x.TimeOut).First().TimeOut.Value - g.Min(y => y.CreatedDate)).Days:D2}D " +
                              $"{(g.Where(x => x.TimeOut.HasValue).OrderByDescending(x => x.TimeOut).First().TimeOut.Value - g.Min(y => y.CreatedDate)).Hours:D2}H " +
                              $"{(g.Where(x => x.TimeOut.HasValue).OrderByDescending(x => x.TimeOut).First().TimeOut.Value - g.Min(y => y.CreatedDate)).Minutes:D2}M"
                            : null
                    })
                    .ToList();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Check-In Filo Reports");

                    var headers = new List<string>
                    {
                        "User",
                        "Time In",
                        "Time Out",
                        "Duration"
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


                    for (var index = 0; index < consolidatedData.Count; index++)
                    {
                        var row = worksheet.Row(index + 2);


                        row.Cell(1).Value = consolidatedData[index].User;
                        row.Cell(2).Value = consolidatedData[index].TimeIn.ToString("MM/dd/yy HH:mm:ss");
                        row.Cell(3).Value = consolidatedData[index].TimeOut?.ToString("MM/dd/yy HH:mm:ss") ?? "";
                        row.Cell(4).Value = consolidatedData[index].Duration;

                    }

                    worksheet.Columns().AdjustToContents();

                    var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    string fileName = $"CheckIns_Filo_Reports_{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }
    }
}
