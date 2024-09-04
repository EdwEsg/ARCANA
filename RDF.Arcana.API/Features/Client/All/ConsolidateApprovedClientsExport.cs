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
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public string Search { get; set; }
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

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(bn => bn.BusinessName.Contains(request.Search));
            }

            var consolidate = await query.ToListAsync(cancellationToken);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Approved Clients");

                var headers = new List<string>
                {
                    "Id",
                    "Business Name"
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
