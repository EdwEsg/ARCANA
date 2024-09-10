using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.All;

[Route("api/approved-clients-export"), ApiController]
public class ConsolidateApprovedClientsExport : ControllerBase
{
    private readonly IMediator _mediator;
    public ConsolidateApprovedClientsExport(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ExportApprovedClients([FromQuery] ConsolidateApprovedClientsExportCommmand command)
    {
        try
        {
            var result = await _mediator.Send(command);

            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class ConsolidateApprovedClientsExportCommmand : IRequest<IActionResult>
    {

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class Handler : IRequestHandler<ConsolidateApprovedClientsExportCommmand, IActionResult>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle(ConsolidateApprovedClientsExportCommmand request, CancellationToken cancellationToken)
        {
            var query = _context.Clients
                .Include(oa => oa.OwnersAddress)
                .Include(ba => ba.BusinessAddress)
                .Include(c => c.Cluster)
                .Include(st => st.StoreType)
                .Include(t => t.Term)
                    .ThenInclude(t => t.Terms)
                .Include(t => t.Term)
                    .ThenInclude(td => td.TermDays)
                .Include(b => b.BookingCoverages)
                .Include(f => f.FixedDiscounts)
                .Include(pm => pm.PriceMode)
                .Include(f => f.Freezer)
                .Where(a => a.RegistrationStatus == Status.Approved &&
                            a.CreatedAt >= request.DateFrom &&
                            a.CreatedAt <= request.DateTo)
                .AsSplitQuery()
                .AsNoTracking();

            var consolidate = await query.ToListAsync(cancellationToken);


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Approved Clients");

                var headers = new List<string>
                {
                    "Id",
                    "Business Name",
                    "Full Name",
                    "Owner's Address",
                    "Phone Number",
                    "Date of Birth",
                    "Email Address",
                    "Tin Number",
                    "Represantative Name",
                    "Representative Position",
                    "Business Address",
                    "Cluster",
                    "Customer Type",
                    "Origin",
                    "Store Type",
                    "Terms",
                    "Credit Limit",
                    "Term Days",
                    "Withholding Isuuance",
                    "Direct Delivery",
                    "Booking Coverage",
                    "Created At",
                    "Fixed Discount",
                    "Variable Discount",
                    "Price Mode",
                    "Freezer Tag"



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

                    row.Cell(1).Value = consolidate[index].Id;
                    row.Cell(2).Value = consolidate[index].BusinessName;
                    row.Cell(3).Value = consolidate[index].Fullname;
                    row.Cell(4).Value = $"{consolidate[index].OwnersAddress?.HouseNumber ?? ""} " +
                                        $"{consolidate[index].OwnersAddress?.StreetName ?? ""} " +
                                        $"{consolidate[index].OwnersAddress?.Barangay ?? ""}, " +
                                        $"{consolidate[index].OwnersAddress?.City ?? ""}, " +
                                        $"{consolidate[index].OwnersAddress?.Province ?? ""}";
                    row.Cell(5).Value = consolidate[index].PhoneNumber;
                    row.Cell(6).Value = consolidate[index].DateOfBirth != null
                        ? consolidate[index].DateOfBirth.ToString("MMM d, yyyy")
                        : string.Empty;
                    row.Cell(7).Value = consolidate[index].EmailAddress;
                    row.Cell(8).Value = consolidate[index].TinNumber;
                    row.Cell(9).Value = consolidate[index].RepresentativeName;
                    row.Cell(10).Value = consolidate[index].RepresentativePosition;
                    row.Cell(11).Value = $"{consolidate[index].BusinessAddress?.HouseNumber ?? ""} " +
                                         $"{consolidate[index].BusinessAddress?.StreetName ?? ""} " +
                                         $"{consolidate[index].BusinessAddress?.Barangay ?? ""}, " +
                                         $"{consolidate[index].BusinessAddress?.City ?? ""}, " +
                                         $"{consolidate[index].BusinessAddress?.Province ?? ""}";
                    row.Cell(12).Value = $"{consolidate[index].Cluster?.ClusterType ?? ""} ";
                    row.Cell(13).Value = consolidate[index].CustomerType;
                    row.Cell(14).Value = consolidate[index].Origin;
                    row.Cell(15).Value = $"{consolidate[index].StoreType?.StoreTypeName ?? ""} ";
                    row.Cell(16).Value = $"{consolidate[index].Term.Terms?.TermType ?? ""} ";
                    row.Cell(17).Value = $"{consolidate[index].Term?.CreditLimit.ToString() ?? ""} ";
                    row.Cell(18).Value = $"{consolidate[index].Term.TermDays?.Days.ToString() ?? ""} ";
                    row.Cell(19).Value = $"{consolidate[index].Term?.WithholdingIssuance ?? ""} ";
                    row.Cell(20).Value = consolidate[index].DirectDelivery == true ? "Yes" : "No";
                    row.Cell(21).Value = $"{consolidate[index].BookingCoverages?.BookingCoverage ?? ""} ";
                    row.Cell(22).Value = consolidate[index].CreatedAt != null
                        ? consolidate[index].CreatedAt.ToString("MMM d, yyyy")
                        : string.Empty;
                    if (consolidate[index].FixedDiscounts != null && consolidate[index].FixedDiscounts.DiscountPercentage != null)
                    {
                        decimal fixedDiscount = (decimal)consolidate[index].FixedDiscounts.DiscountPercentage;
                        row.Cell(23).Value = (fixedDiscount * 100).ToString("0.00") + "%";
                    }
                    else
                    {
                        row.Cell(23).Value = string.Empty;
                    }
                    row.Cell(24).Value = consolidate[index].VariableDiscount == true ? "Yes" : string.Empty;
                    row.Cell(25).Value = $"{consolidate[index].PriceMode?.PriceModeDescription ?? ""} ";
                    row.Cell(26).Value = $"{consolidate[index].Freezer?.AssetTag ?? ""} ";




                }

                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                string fileName = $"ArcanaApprovedClients_{request.DateFrom:MMM d, yyyy}_{request.DateTo:MMM d, yyyy}.xlsx";
                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = fileName
                };
            }
        }
    }
}
