using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Reports.Inventory_Reports
{
    [Route("api/arcana-transfer-out-reports"), ApiController]
    public class TransferOutReport : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransferOutReport(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ArcanaTransferReports([FromQuery] TransferOutReportCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AccessBy = userId;
                }

                var result = await _mediator.Send(command);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class TransferOutReportCommand : IRequest<IActionResult>
        {
            public int AccessBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class Handler : IRequestHandler<TransferOutReportCommand, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(TransferOutReportCommand request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                var query = _context.TransferOrders
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(u => u.CreatedBy)
                        .ThenInclude(c => c.CdoCluster)
                    .Where(t => t.Status == Status.Received);

                if (user.UserRoles.UserRoleName != Roles.Cdo)
                {
                    query = query.Where(t => t.TransactionDate >= request.DateFrom && t.TransactionDate < adjustedDateTo);
                }

                if (request.ClusterId != null)
                {
                    query = query.Where(t => t.CreatedBy.CdoCluster.ClusterId == request.ClusterId);
                }

                else
                {
                    query = query.Where(t => t.CreatedBy.Id == request.AccessBy);
                }

                var finalQuery = query.SelectMany(t => t.TransferOrderItems)
                    .Select(i => new
                    {
                        TransferItems = i,
                        TransferId = i.TransferOrder.Id,
                        CreatedByName = i.TransferOrder.CreatedBy.Fullname,
                        TransferToName = i.TransferOrder.TransferTo.Fullname,
                        TransferDate = i.TransferOrder.TransactionDate,
                        MeatType = i.Item.MeatType.MeatTypeName,
                        ProductSubCategory = i.Item.ProductSubCategory.ProductSubCategoryName,
                        ModifiedDate = i.TransferOrder.ModifiedDate
                    });

                var consolidate = await finalQuery.ToListAsync(cancellationToken);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Arcana_Transfer_Out");

                    var headers = new List<string>
                    {
                        "Series",
                        "Id",
                        "Receiving Date",
                        "Supplier Code",
                        "Supplier Name",
                        "Item Code",
                        "Item Description",
                        "UOM",
                        "Category",
                        "Product Category",
                        "Quantity",
                        "Slab",
                        "Production Date",
                        "Farm Source",
                        "Description",
                        "Reference",
                        "Reason",
                        "Item Reference",
                        "Account Title",
                        "Company Code",
                        "Company",
                        "Department Code",
                        "Department",
                        "Location Code",
                        "Location",
                        "Account Code",
                        "Account",
                        "Warehouse Code",
                        "Transaction Date",
                        "Transfer By",
                        "Transfer To"
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

                        row.Cell(1).Value = consolidate[index].TransferId;
                        row.Cell(2).Value = consolidate[index].TransferItems.Id;
                        row.Cell(3).Value = consolidate[index].ModifiedDate;
                        row.Cell(4).Value = "RDF";
                        row.Cell(5).Value = "RDF";
                        row.Cell(6).Value = consolidate[index].TransferItems.ItemCode;
                        row.Cell(7).Value = consolidate[index].TransferItems.ItemDescription;
                        row.Cell(8).Value = consolidate[index].TransferItems.Uom;
                        row.Cell(9).Value = consolidate[index].MeatType;
                        row.Cell(10).Value = consolidate[index].ProductSubCategory;
                        row.Cell(11).Value = consolidate[index].TransferItems.Quantity;
                        row.Cell(12).Value = "";
                        row.Cell(13).Value = consolidate[index].TransferItems.Bbd;
                        row.Cell(14).Value = "";
                        row.Cell(15).Value = "";
                        row.Cell(16).Value = "";
                        row.Cell(17).Value = "";
                        row.Cell(18).Value = "";
                        row.Cell(19).Value = "";
                        row.Cell(20).Value = "31";
                        row.Cell(21).Value = "Fresh Options";
                        row.Cell(22).Value = "";
                        row.Cell(23).Value = "";
                        row.Cell(24).Value = "";
                        row.Cell(25).Value = "";
                        row.Cell(26).Value = "";
                        row.Cell(27).Value = "";
                        row.Cell(28).Value = "";
                        row.Cell(29).Value = consolidate[index].TransferDate;
                        row.Cell(30).Value = consolidate[index].CreatedByName;
                        row.Cell(31).Value = consolidate[index].TransferToName;

                        for (int col = 1; col <= 23; col++)
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

                    string fileName = $"Arcana_Transfer_Out_Receiving{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }

            }
        }
    }
}
