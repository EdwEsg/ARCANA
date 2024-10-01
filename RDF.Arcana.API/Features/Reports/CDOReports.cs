using ClosedXML.Excel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using System.Security.Claims;


namespace RDF.Arcana.API.Features.Reports;
[Route("api/CDO-reports"), ApiController]

public class CDOReports : ControllerBase
{
    private readonly IMediator _mediator;

    public CDOReports(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> CDOReport([FromQuery] CDOReportsCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);

            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class CDOReportsCommand : IRequest<IActionResult>
    {
        public int? AddedBy { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class Handler : IRequestHandler<CDOReportsCommand, IActionResult>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle(CDOReportsCommand request, CancellationToken cancellationToken)
        {
            var userDictionary = await _context.Users
                .Where(u => _context.TransactionItems.Select(ti => ti.AddedBy).Distinct().Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.Fullname, cancellationToken);

            var query = _context.TransactionItems
                .Include(t => t.Transaction)
                    .ThenInclude(ts => ts.TransactionSales)
                .Include(i => i.Item)
                .Include(t => t.Transaction)
                    .ThenInclude(c => c.Client)
                        .ThenInclude(b => b.BusinessAddress)
                .Include(t => t.Transaction)
                    .ThenInclude(c => c.Client)
                        .ThenInclude(cl => cl.Cluster)
                            .ThenInclude(cdo => cdo.CdoClusters)
                                .ThenInclude(u => u.User)
                .Include(t => t.Transaction)
                    .ThenInclude(c => c.Client)
                        .ThenInclude(st => st.StoreType)
                .AsSplitQuery()
                .AsNoTracking();

            if (request.AddedBy == 1)
            {
                query = query.Where(ti => ti.CreatedAt >= request.DateFrom && ti.CreatedAt <= request.DateTo);
            }
            else
            {
                query = query.Where(ti => ti.CreatedAt >= request.DateFrom && ti.CreatedAt <= request.DateTo
                                          && ti.AddedBy == request.AddedBy);

                var hasMatchingItems = await query.AnyAsync(cancellationToken);
                if (!hasMatchingItems)
                {
                    return new UnauthorizedResult();
                }
            }

            var consolidate = await query.ToListAsync(cancellationToken);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CDO Reports");

                var headers = new List<string>
                {
                    "Remarks (Old/New)",
                    "Month",
                    "Date",
                    "Outlet Code",
                    "CI#",
                    "SI#",
                    "Item Code",
                    "Quantity",
                    "Discount",
                    "Price",
                    "Amount",
                    "Discounted Price",
                    "Discounted Amount",
                    "Item Description",
                    "Outlet",
                    "Municipality",
                    "Province",
                    "Area",
                    "New Cluster",
                    "Customer Type",
                    "Channel",
                    "Status",
                    "Region"
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

                    row.Cell(1).Value = "";
                    row.Cell(2).Value = consolidate[index].CreatedAt.ToString("MMM");
                    row.Cell(3).Value = consolidate[index].CreatedAt.ToString("dd/MM/yyyy");
                    row.Cell(4).Value = "N/A";
                    row.Cell(5).Value = consolidate[index].Transaction.InvoiceType == "Charge" ? consolidate[index].Transaction.InvoiceNo : "";
                    row.Cell(6).Value = consolidate[index].Transaction.InvoiceType == "Sales" ? consolidate[index].Transaction.InvoiceNo : "";
                    row.Cell(7).Value = consolidate[index].Item.ItemCode;

                    //Qty to Discounted Price
                    row.Cell(8).Value = consolidate[index].Quantity;
                    row.Cell(9).Value = ((consolidate[index].Transaction.TransactionSales.Discount * 100) +
                                        (consolidate[index].Transaction.TransactionSales.SpecialDiscount * 100)).ToString("0.##") + "%";
                    row.Cell(10).Value = consolidate[index].UnitPrice;
                    row.Cell(11).Value = consolidate[index].Amount;

                    decimal discountPercentage = consolidate[index].Transaction.TransactionSales.Discount +
                            consolidate[index].Transaction.TransactionSales.SpecialDiscount;
                    decimal discountedPrice = consolidate[index].UnitPrice * (1 - discountPercentage);
                    row.Cell(12).Value = discountedPrice;

                    decimal totalDiscountedAmount = discountedPrice * consolidate[index].Quantity;
                    row.Cell(13).Value = totalDiscountedAmount;
                    //End of Qty to Discounted Price

                    row.Cell(14).Value = consolidate[index].Item.ItemDescription;
                    row.Cell(15).Value = consolidate[index].Transaction.Client.BusinessName;
                    row.Cell(16).Value = consolidate[index].Transaction.Client.BusinessAddress.City;
                    row.Cell(17).Value = consolidate[index].Transaction.Client.BusinessAddress.Province;
                    row.Cell(18).Value = consolidate[index].Transaction.Client.BusinessAddress.Province;
                    row.Cell(19).Value = consolidate[index].Transaction.Client.Cluster.ClusterType;
                    row.Cell(20).Value = consolidate[index].Transaction.Client.CustomerType;
                    row.Cell(21).Value = consolidate[index].Transaction.Client.StoreType.StoreTypeName;

                    bool status = consolidate[index].IsActive;
                    row.Cell(22).Value = status is true ? "Active" : "Delisted";

                    row.Cell(23).Value = userDictionary.ContainsKey(consolidate[index].AddedBy)
                        ? userDictionary[consolidate[index].AddedBy]
                        : "Unknown";

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

                string fileName = $"CDO_Reports_{request.DateFrom:MMM d, yyyy}-{request.DateTo:MMM d, yyyy}.xlsx";
                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = fileName
                };
            }

        }
    }
}
