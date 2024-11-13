using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/check-ins-reports"), ApiController]
    public class CheckInReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public CheckInReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CheckInsResult([FromQuery] CheckInReportsQuery query)
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

        public class CheckInReportsQuery : IRequest<IActionResult>
        {
            public int? AddedBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class Handler : IRequestHandler<CheckInReportsQuery, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(CheckInReportsQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var query = _context.CheckIns
                    .Include(c => c.Client)
                        .ThenInclude(b => b.BusinessAddress)
                    .Include(c => c.CreatedBy)
                        .ThenInclude(u => u.UserRoles)
                    .Include(c => c.CreatedBy)
                        .ThenInclude(u => u.CdoCluster)
                    .Where(t => t.CreatedDate >= request.DateFrom && t.CreatedDate < adjustedDateTo)
                    .AsQueryable();

                var userClusters = _context.CdoClusters
                    .FirstOrDefault(x => x.UserId == request.AddedBy);

                //per CDO viewing
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

                var consolidate = await query.ToListAsync(cancellationToken);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Check-In Reports");

                    var headers = new List<string>
                    {
                        "User",
                        "Time In",
                        "Time Out",
                        "Business Name",
                        "Business Owner",
                        "Barangay",
                        "City",
                        "Province",
                        "Latitude",
                        "Longitude"
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

                        row.Cell(1).Value = consolidate[index].CreatedBy.Fullname;
                        row.Cell(2).Value = consolidate[index].CreatedDate.ToString("MM/dd/yy HH:mm:ss");
                        row.Cell(3).Value = consolidate[index].TimeOut?.ToString("MM/dd/yy HH:mm:ss") ?? "";
                        row.Cell(4).Value = consolidate[index].Client?.BusinessName ?? consolidate[index].BusinessNameOthers;
                        row.Cell(5).Value = consolidate[index].Client?.Fullname ?? consolidate[index].FullNameOthers;
                        row.Cell(6).Value = consolidate[index].Client?.BusinessAddress?.Barangay ?? consolidate[index].BarangayOthers;
                        row.Cell(7).Value = consolidate[index].Client?.BusinessAddress?.City ?? consolidate[index].CityOthers;
                        row.Cell(8).Value = consolidate[index].Client?.BusinessAddress?.Province ?? consolidate[index].ProvinceOthers;
                        row.Cell(9).Value = consolidate[index].Latitude;
                        row.Cell(10).Value = consolidate[index].Longitude;

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

                    string fileName = $"CheckIns_Reports_{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }

            }
        }
    }
}
