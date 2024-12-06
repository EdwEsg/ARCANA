using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-inventory-mrp"), ApiController]
    public class GetInventoryMrp : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetInventoryMrp(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetInventoryMrpQuery query)
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;
            }

            try
            {
                var mo = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    mo.CurrentPage,
                    mo.PageSize,
                    mo.TotalCount,
                    mo.TotalPages,
                    mo.HasNextPage,
                    mo.HasPreviousPage);
                var result = new
                {
                    mo,
                    mo.CurrentPage,
                    mo.PageSize,
                    mo.TotalCount,
                    mo.TotalPages,
                    mo.HasNextPage,
                    mo.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetInventoryMrpQuery : UserParams, IRequest<PagedList<GetInventoryMrpResult>>
        {
            public int AccessBy { get; set; }
        }

        public class GetInventoryMrpResult
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal? Receiving { get; set; }
            public decimal? In { get; set; }
            public decimal? Out { get; set; }
            public decimal? Freebie { get; set; }
            public decimal? Sampling { get; set; }
            public decimal? Issue { get; set; }
            public decimal? Replace { get; set; }
            public decimal? Return { get; set; }
            public decimal? Soh { get; set; }
        }

        public class Handler : IRequestHandler<GetInventoryMrpQuery, PagedList<GetInventoryMrpResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetInventoryMrpResult>> Handle(GetInventoryMrpQuery request, CancellationToken cancellationToken)
            {
                var moveOrderItems = _context.MoveOrderItems.AsQueryable();

                if (request.AccessBy != 1)
                {
                    moveOrderItems = moveOrderItems.Where(s => s.CreatedBy.Id == request.AccessBy);
                }

                var groupReceiving = moveOrderItems
                    .Where(x => x.Reason == null)
                    .GroupBy(x => new
                    {
                        x.ItemCode
                    })
                    .Select(x => new
                    {
                        ItemCode = x.Key.ItemCode,
                        ReceivingActualQuantity = x.Sum(x => x.ActualQuantity)
                    });


                var result = moveOrderItems
                    .Where(x => x.Reason == null)
                    .GroupBy(x => new
                    {
                        x.ItemCode
                    })
                    .Select(x => new GetInventoryMrpResult
                    {
                        ItemCode = x.Key.ItemCode,
                        ItemDescription = x.Select(g => g.Item.ItemDescription).FirstOrDefault(),
                        Receiving = groupReceiving.Where(gr => gr.ItemCode == x.Key.ItemCode)
                            .Select(gr => gr.ReceivingActualQuantity).FirstOrDefault(),
                        Soh = groupReceiving.Where(gr => gr.ItemCode == x.Key.ItemCode)
                            .Select(gr => gr.ReceivingActualQuantity).FirstOrDefault()
                    })
                    .OrderBy(x => x.ItemCode);

                return await PagedList<GetInventoryMrpResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
