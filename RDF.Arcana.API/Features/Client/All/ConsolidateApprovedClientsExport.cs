using ClosedXML.Excel;
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
                .Where(a => a.RegistrationStatus == Status.Approved &&
                            a.CreatedAt >= request.DateFrom &&
                            a.CreatedAt <= request.DateTo);

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
                    "Direct Delivery",
                    "Booking Coverage",
                    "Created At",
                    "Fixed Discount",
                    "Variable Discount",
                    "Price Mode",
                    "Freezer"



                };

                for (var index = 0; index < headers.Count; index++)
                {
                    worksheet.Cell(1, index + 1).Value = headers[index];
                }

                for (var index = 0; index < consolidate.Count; index++)
                {
                    var row = worksheet.Row(index + 2);

                    row.Cell(1).Value = consolidate[index].Id;
                    row.Cell(2).Value = consolidate[index].BusinessName;
                    row.Cell(3).Value = consolidate[index].Fullname;
                    row.Cell(4).Value = consolidate[index].OwnersAddress == null ? string.Empty : consolidate[index].OwnersAddress.ToString();
                    row.Cell(5).Value = consolidate[index].PhoneNumber;
                    row.Cell(6).Value = consolidate[index].DateOfBirth;
                    row.Cell(7).Value = consolidate[index].EmailAddress;
                    row.Cell(8).Value = consolidate[index].TinNumber;
                    row.Cell(9).Value = consolidate[index].RepresentativeName;
                    row.Cell(10).Value = consolidate[index].RepresentativePosition;
                    row.Cell(11).Value = consolidate[index].BusinessAddress == null ? string.Empty : consolidate[index].OwnersAddress.ToString();
                    row.Cell(12).Value = consolidate[index].Cluster == null ? string.Empty : consolidate[index].OwnersAddress.ToString();
                    row.Cell(13).Value = consolidate[index].CustomerType;
                    row.Cell(14).Value = consolidate[index].Origin;
                    row.Cell(15).Value = consolidate[index].StoreType == null ? string.Empty : consolidate[index].OwnersAddress.ToString();
                    row.Cell(16).Value = consolidate[index].Terms == null ? string.Empty : consolidate[index].Terms.ToString();
                    row.Cell(17).Value = consolidate[index].DirectDelivery == null ? string.Empty : consolidate[index].DirectDelivery.ToString();
                    row.Cell(18).Value = consolidate[index].BookingCoverageId == null ? string.Empty : consolidate[index].BookingCoverageId.ToString();
                    row.Cell(19).Value = consolidate[index].CreatedAt == null ? string.Empty : consolidate[index].CreatedAt.ToString();
                    row.Cell(20).Value = consolidate[index].FixedDiscountId == null ? string.Empty : consolidate[index].FixedDiscountId.ToString();
                    row.Cell(21).Value = consolidate[index].VariableDiscount == null ? string.Empty : consolidate[index].VariableDiscount.ToString();
                    row.Cell(22).Value = consolidate[index].PriceMode == null ? string.Empty : consolidate[index].PriceMode.ToString();
                    row.Cell(23).Value = consolidate[index].FreezerId == null ? string.Empty : consolidate[index].FreezerId.ToString();




                }

                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                string fileName = $"ConsolidatedApprovedClients_{request.DateFrom:yyyyMMdd}_{request.DateTo:yyyyMMdd}.xlsx";
                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = fileName
                };
            }
        }
    }
}
