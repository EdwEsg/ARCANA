
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Linq;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-all-items-inventory"), ApiController]
    public class GetAllItemsInInventory : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllItemsInInventory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllItemsInInventoryQuery query)
        {

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetAllItemsInInventoryQuery : IRequest<Result> 
        {
            public string Search { get; set; }
        }

        public class GetAllItemsInInventoryResult
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
            public decimal? CurrentPrice { get; set; }
            public bool? IsClearPack { get; set; }
        }

        public class Handler : IRequestHandler<GetAllItemsInInventoryQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllItemsInInventoryQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PriceModeItems> priceModeItems = _context.PriceModeItems
                    .Include(x => x.PriceMode)
                    .Include(i => i.Item)
                        .ThenInclude(x => x.Uom)
                    .Include(i => i.Item)
                        .ThenInclude(x => x.MeatType)
                    .Include(i => i.Item)
                        .ThenInclude(x => x.ProductSubCategory)
                    .Where(x => x.PriceMode.IsActive);
                
                if (!string.IsNullOrEmpty(request.Search))
                {
                    priceModeItems = priceModeItems.Where(p =>
                        p.Item.ItemCode.Contains(request.Search) ||
                        p.Item.ItemDescription.Contains(request.Search));
                }

                var groupedResult = await priceModeItems
                    .GroupBy(pmi => new
                    {
                        pmi.Item.Id,
                        pmi.Item.ItemCode,
                        pmi.Item.ItemDescription,
                        pmi.Item.ItemImageLink,
                        pmi.Item.Uom.UomCode,
                        pmi.Item.MeatType.MeatTypeName,
                        pmi.Item.ProductSubCategory.ProductSubCategoryName
                    })
                    .Select(group => new GetAllItemsInInventoryResult
                    {
                        
                        ItemCode = group.Key.ItemCode,
                        ItemDescription = group.Key.ItemDescription,
                        ItemImageLink = group.Key.ItemImageLink,
                        Uom = group.Key.UomCode,
                        MeatType = group.Key.MeatTypeName,
                        ProductSubCategoryName = group.Key.ProductSubCategoryName,
                        ItemId = group.Key.Id,
                        
                        PriceModeItemId = group.FirstOrDefault().Id, 
                        PriceModeId = group.FirstOrDefault().PriceModeId,
                        PriceModeCode = group.FirstOrDefault().PriceMode.PriceModeCode,
                        IsActive = group.All(pmi => pmi.IsActive), 
                        CurrentPrice = group
                            .SelectMany(pmi => pmi.ItemPriceChanges
                                .Where(pc => pc.EffectivityDate <= DateTime.Now)
                                .OrderByDescending(pc => pc.EffectivityDate)
                                .Select(pc => (decimal?)pc.Price))
                            .FirstOrDefault() ?? 0m, 
                        IsClearPack = group.FirstOrDefault().IsClearPack
                    })
                    .ToListAsync(cancellationToken);

                return Result.Success(groupedResult);
            }
        }
    }
}
