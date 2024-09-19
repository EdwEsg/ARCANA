using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Price_Mode;

[Route("api/price-mode-with-items"), ApiController]
public class AddNewPriceModeWithItems : ControllerBase
{
    private readonly IMediator _mediator;
    public AddNewPriceModeWithItems(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddNewPriceModeWithItemsCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    public class AddNewPriceModeWithItemsCommand : IRequest<Result>
    {
        public string PriceModeCode { get; set; }
        public string PriceModeDescription { get; set; }
        public int AddedBy { get; set; }
        public ICollection<ItemWithPrice> Items { get; set; }
        public class ItemWithPrice
        {
            public int PriceModeId { get; set; }
            public int ItemId { get; set; }
            public decimal Price { get; set; }
            public string Remarks { get; set; }
            public int AddedBy { get; set; }
        }

    }

    public class Handler : IRequestHandler<AddNewPriceModeWithItemsCommand, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public Task<Result> Handle(AddNewPriceModeWithItemsCommand request, CancellationToken cancellationToken)
        {
            var priceModeWithItems = _context.PriceModeItems
                .Include(p => p.PriceMode)
                .Include(pmi => pmi.ItemPriceChanges)
                .Include(i => i.Item);

            var priceMode = new PriceMode
            {
                PriceModeCode = request.PriceModeCode,
                PriceModeDescription = request.PriceModeDescription,
                AddedBy = request.AddedBy
            };

            _context.Add(priceMode);
            _context.SaveChanges();

            foreach (var item in request.Items)
            {
                var priceModeItems = new PriceModeItems
                {
                    PriceModeId = priceMode.Id,
                    ItemId = item.ItemId,
                    AddedBy = request.AddedBy
                };

                _context.Add(priceModeItems);
                _context.SaveChanges();

                var itemPrice = new ItemPriceChange
                {
                    PriceModeItemId = priceModeItems.Id,
                    Price = item.Price,
                    EffectivityDate = DateTime.Now, 
                    AddedBy = request.AddedBy,
                    Remarks = item.Remarks
                };

                _context.Add(itemPrice);
                _context.SaveChanges();
            }

            return Task.FromResult(Result.Success());

        }
    }
}
