using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Reports
{
    [Route("api/arcana-mo-reports"), ApiController]
    public class ReceivingReport : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReceivingReport(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ArcanaMoReports([FromQuery] ReceivingReportCommand command)
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

        public class ReceivingReportCommand : IRequest<IActionResult>
        {
            public int AccessBy { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class Handler : IRequestHandler<ReceivingReportCommand, IActionResult>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Handle(ReceivingReportCommand request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                IQueryable<MoveOrderItem> query = _context.MoveOrderItems
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Where(m => m.ActualQuantity != null)
                    .Include(m => m.MoveOrder)
                    .Include(u => u.Uom)
                    .Include(i => i.Item)
                        .ThenInclude(mt => mt.MeatType)
                    .Include(i => i.Item)
                        .ThenInclude(pr => pr.ProductSubCategory)
                    .Include(c => c.CreatedBy);

                if (user.UserRoles.UserRoleName != Roles.Cdo) 
                {
                    query = query.Where(ti => ti.MoveOrder.CreatedDate >= request.DateFrom && ti.MoveOrder.CreatedDate < adjustedDateTo);
                }

                if (request.ClusterId != null)
                {
                    query = query.Where(mo => mo.CreatedBy.CdoCluster.ClusterId == request.ClusterId);
                }

                else
                {
                    query = query.Where(mo => mo.CreatedBy.Id == request.AccessBy);
                }

                var consolidate = await query.ToListAsync(cancellationToken);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Arcana_Received_MoveOrders");

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
                        "Encoded By"
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
                        row.Cell(2).Value = consolidate[index].MoveOrder.MoveOrderIdExternal;
                        row.Cell(3).Value = consolidate[index].MoveOrder.CreatedDate.ToString("dd/MM/yyyy");
                        row.Cell(4).Value = "RDF";
                        row.Cell(5).Value = "RDF";
                        row.Cell(6).Value = consolidate[index].ItemCode;
                        row.Cell(7).Value = consolidate[index].Item.ItemDescription;
                        row.Cell(8).Value = consolidate[index].Uom.UomDescription;
                        row.Cell(9).Value = consolidate[index].Item.MeatType.MeatTypeName;
                        row.Cell(10).Value = consolidate[index].Item.ProductSubCategory.ProductSubCategoryName;
                        row.Cell(11).Value = consolidate[index].ActualQuantity;
                        row.Cell(12).Value = "";
                        row.Cell(13).Value = consolidate[index].ProductionDate;
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
                        row.Cell(29).Value = consolidate[index].MoveOrder.TransactionDate;
                        row.Cell(30).Value = consolidate[index].CreatedBy.Fullname;

                        //for centering the numeric value for better readability
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

                    string fileName = $"Arcana_MoveOrder_Receiving{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                    return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }
    }
}
