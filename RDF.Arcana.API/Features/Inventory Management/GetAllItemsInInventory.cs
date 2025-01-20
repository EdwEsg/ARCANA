
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
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
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;
            }

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
            public int AccessBy { get; set; }
        }

        public class GetAllItemsInInventoryResult
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal? AvailQuantity { get; set; }
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
                var moveOrderItems = _context.MoveOrderItems
                    .Where(rq => rq.RemainingQuantity > 0 && rq.MoveOrder.CreatedById == request.AccessBy)
                    .GroupBy(x => new { x.ItemCode, x.Item.ItemDescription })
                    .Select(g => new GetAllItemsInInventoryResult
                    {
                        ItemCode = g.Key.ItemCode,
                        ItemDescription = g.Key.ItemDescription,
                        AvailQuantity = g.Sum(x => x.RemainingQuantity)
                    });

                var transferInItems = _context.TransferOrderItems
                    .Where(rq => rq.RemainingQuantity > 0 && rq.TransferOrder.CreatedById == request.AccessBy)
                    .GroupBy(x => new { x.ItemCode, x.Item.ItemDescription })
                    .Select(g => new GetAllItemsInInventoryResult
                    {
                        ItemCode = g.Key.ItemCode,
                        ItemDescription = g.Key.ItemDescription,
                        AvailQuantity = g.Sum(x => x.RemainingQuantity)
                    });

                var returnOrderItems = _context.ReturnOrderItems
                    .Where(rq => rq.RemainingQuantity > 0 && rq.ReturnOrder.CreatedbyId == request.AccessBy)
                    .Select(rq => new
                    {
                        rq.Item.ItemCode,
                        rq.Item.ItemDescription,
                        rq.RemainingQuantity
                    })
                    .GroupBy(x => new { x.ItemCode, x.ItemDescription })
                    .Select(g => new GetAllItemsInInventoryResult
                    {
                        ItemCode = g.Key.ItemCode,
                        ItemDescription = g.Key.ItemDescription,
                        AvailQuantity = g.Sum(x => x.RemainingQuantity)
                    });

                var consolidatedItems = moveOrderItems
                    .Concat(transferInItems)
                    .Concat(returnOrderItems)
                    .GroupBy(x => new { x.ItemCode, x.ItemDescription })
                    .Select(g => new GetAllItemsInInventoryResult
                    {
                        ItemCode = g.Key.ItemCode,
                        ItemDescription = g.Key.ItemDescription,
                        AvailQuantity = g.Sum(x => x.AvailQuantity)
                    })
                    .ToList();

                return Result.Success(consolidatedItems);
            }
        }
    }
}
