using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Price_Mode
{
    [Route("api/price-mode-items"), ApiController]
    public class GetAllItemsForPriceModeByPriceModeCode : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllItemsForPriceModeByPriceModeCode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllItemsForPriceModeQuery query)
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;
            }

            try
            {

                var priceModeItems = await _mediator.Send(query);

                Response.AddPaginationHeader(
                priceModeItems.CurrentPage,
                priceModeItems.PageSize,
                priceModeItems.TotalCount,
                priceModeItems.TotalPages,
                priceModeItems.HasPreviousPage,
                priceModeItems.HasNextPage
            );
                var results = new
                {

                    priceModeItems,
                    priceModeItems.CurrentPage,
                    priceModeItems.PageSize,
                    priceModeItems.TotalCount,
                    priceModeItems.TotalPages,
                    priceModeItems.HasPreviousPage,
                    priceModeItems.HasNextPage
                };

                var successResult = Result.Success(results);
                return Ok(successResult);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetAllItemsForPriceModeQuery : UserParams, IRequest<PagedList<GetAllItemsForPriceModeResult>>
        {
            public int? PriceModeId { get; set; }
            public string Search { get; set; }
            public bool? Status { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetAllItemsForPriceModeResult
        {
            public int PriceModeItemId { get; set; }
            public int PriceModeId { get; set; }
            public string PriceModeCode { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string ItemImageLink { get; set; }
            public string Uom { get; set; }
            public string ProductSubCategoryName { get; set; }
            public string MeatType { get; set; }
            public bool IsActive { get; set; }
            public string ModifiedBy { get; set; }
            public decimal? CurrentPrice { get; set; }
            public bool? IsClearPack { get; set; }
            public decimal? AvailQuantity { get; set; }

        }

        public class Handler : IRequestHandler<GetAllItemsForPriceModeQuery, PagedList<GetAllItemsForPriceModeResult>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetAllItemsForPriceModeResult>> Handle(GetAllItemsForPriceModeQuery request, CancellationToken cancellationToken)
            {
                
                var moveOrderItems = _context.MoveOrderItems
                    .Where(rq => rq.RemainingQuantity > 0 && rq.MoveOrder.CreatedById == request.AccessBy)
                    .Select(x => new
                    {
                        x.Item.ItemCode,
                        x.Item.ItemDescription,
                        RemainingQuantity = (decimal?)x.RemainingQuantity
                    });

                var transferInItems = _context.TransferOrderItems
                    .Where(rq => rq.RemainingQuantity > 0 && rq.TransferOrder.CreatedById == request.AccessBy)
                    .Select(x => new
                    {
                        x.Item.ItemCode,
                        x.Item.ItemDescription,
                        RemainingQuantity = (decimal?)x.RemainingQuantity
                    });

                var returnOrderItems = _context.ReturnOrderItems
                    .Where(rq => rq.RemainingQuantity > 0 && rq.ReturnOrder.CreatedbyId == request.AccessBy)
                    .Select(x => new
                    {
                        x.Item.ItemCode,
                        x.Item.ItemDescription,
                        RemainingQuantity = (decimal?)x.RemainingQuantity
                    });

                var consolidatedQuery =
                    moveOrderItems
                    .Concat(transferInItems)
                    .Concat(returnOrderItems)
                    .GroupBy(g => new { g.ItemCode, g.ItemDescription })
                    .Select(grp => new
                    {
                        ItemCode = grp.Key.ItemCode,
                        ItemDescription = grp.Key.ItemDescription,
                        AvailQuantity = grp.Sum(x => x.RemainingQuantity)
                    });

                IQueryable<PriceModeItems> priceModeItems = _context.PriceModeItems
                    .Include(x => x.PriceMode)
                    .Include(i => i.Item)
                        .ThenInclude(x => x.Uom)
                    .Include(i => i.Item)
                        .ThenInclude(x => x.MeatType)
                    .Include(i => i.Item)
                        .ThenInclude(x => x.ProductSubCategory)
                    .Where(x => x.PriceMode.IsActive);

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    priceModeItems = priceModeItems.Where(pmi =>
                        pmi.Item.ItemCode.Contains(request.Search) ||
                        pmi.Item.ItemDescription.Contains(request.Search) ||
                        pmi.Item.Uom.UomDescription.Contains(request.Search) ||
                        pmi.Item.Uom.UomCode.Contains(request.Search) ||
                        pmi.PriceMode.PriceModeCode.Contains(request.Search) ||
                        pmi.PriceMode.PriceModeDescription.Contains(request.Search));
                }

                if (request.Status is not null)
                {
                    priceModeItems = priceModeItems.Where(pmi => pmi.IsActive == request.Status);
                }

                if (request.PriceModeId is not null)
                {
                    priceModeItems = priceModeItems.Where(pmi =>
                        (pmi.PriceModeId == request.PriceModeId && (pmi.IsClearPack != true)) ||
                        (pmi.PriceModeId == 1 && !_context.PriceModeItems.Any(p =>
                            p.ItemId == pmi.ItemId &&
                            p.PriceModeId == request.PriceModeId &&
                            (p.IsClearPack != true))) ||
                        (pmi.PriceModeId == request.PriceModeId && pmi.IsClearPack == true)
                    );
                }
                else
                {
                    priceModeItems = priceModeItems.Where(pmi => pmi.PriceModeId == 1);
                }

                var query =
                    from pmi in priceModeItems
                    join stock in consolidatedQuery
                        on pmi.Item.ItemCode equals stock.ItemCode
                    where stock.AvailQuantity > 0
                    select new GetAllItemsForPriceModeResult
                    {
                        PriceModeItemId = pmi.Id,
                        PriceModeId = pmi.PriceModeId,
                        PriceModeCode = pmi.PriceMode.PriceModeCode,
                        ItemId = pmi.ItemId,
                        ItemCode = pmi.Item.ItemCode,
                        ItemDescription = pmi.Item.ItemDescription,
                        ItemImageLink = pmi.Item.ItemImageLink,
                        Uom = pmi.Item.Uom.UomCode,
                        MeatType = pmi.Item.MeatType.MeatTypeName,
                        ProductSubCategoryName = pmi.Item.ProductSubCategory.ProductSubCategoryName,
                        IsActive = pmi.IsActive,
                        CurrentPrice = pmi.ItemPriceChanges
                            .OrderBy(pc => pc.EffectivityDate)
                            .First(pc => pc.EffectivityDate <= DateTime.Now).Price,
                        IsClearPack = pmi.IsClearPack,
                        AvailQuantity = stock.AvailQuantity ?? 0
                    };

                var orderedQuery = query.OrderBy(x => x.ItemCode);

                return await PagedList<GetAllItemsForPriceModeResult>
                    .CreateAsync(orderedQuery, request.PageNumber, request.PageSize);
            }
        }
    }
}
