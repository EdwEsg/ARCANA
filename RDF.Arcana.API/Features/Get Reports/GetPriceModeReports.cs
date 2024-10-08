
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Get_Reports
{
    [Route("api/get-price-mode-reports"), ApiController]
    public class GetPriceModeReports : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetPriceModeReports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPriceModeReportsQuery query)
        {
            try
            {
                var transactions = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage);
                var result = new
                {
                    transactions,
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetPriceModeReportsQuery : UserParams, IRequest<PagedList<GetPriceModeReportsResult>> { }

        public class GetPriceModeReportsResult
        {
            public DateTime Date { get; set; }
            public string ItemDescription { get; set; }
            public string PriceMode { get; set; }
            public decimal Price { get; set; }
        }

        public class Handler : IRequestHandler<GetPriceModeReportsQuery, PagedList<GetPriceModeReportsResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<PagedList<GetPriceModeReportsResult>> Handle(GetPriceModeReportsQuery request, CancellationToken cancellationToken)
            {
                var priceMode = _context.PriceModeItems
                    .Include(pm => pm.PriceMode)
                    .Include(i => i.Item)
                    .Include(ipm => ipm.ItemPriceChanges)
                    .Include(u => u.AddedByUser)
                    .OrderBy(iid => iid.ItemId)
                    .AsSplitQuery()
                    .AsNoTracking();

                var result = priceMode.Select(p => new GetPriceModeReportsResult
                {
                    Date = p.CreatedAt,
                    ItemDescription = p.Item.ItemDescription,
                    PriceMode = p.PriceMode.PriceModeDescription,
                    Price = p.ItemPriceChanges.FirstOrDefault().Price,

                });

                return PagedList<GetPriceModeReportsResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
