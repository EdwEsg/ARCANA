using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-wrong-orders"), ApiController]
    public class GetWrongItems : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetWrongItems(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetWrongItemsQuery query)
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

        public class GetWrongItemsQuery : UserParams, IRequest<PagedList<GetWrongItemsResult>> 
        {
            public int? AccessBy { get; set; }
        }
        
        public class GetWrongItemsResult
        {
            public int MoveOrderId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public decimal Quantity { get; set; }
            public string ProductionDate { get; set; }
            public string Reason { get; set; }

        }

        public class Handler : IRequestHandler<GetWrongItemsQuery, PagedList<GetWrongItemsResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetWrongItemsResult>> Handle(GetWrongItemsQuery request, CancellationToken cancellationToken)
            {
                var wrongOrders = _context.MoveOrderItems.AsQueryable();

                if (request.AccessBy != 1)
                {
                    wrongOrders = wrongOrders.Where(x => x.CreatedBy.Id == request.AccessBy);
                }

                var result = wrongOrders
                    .Where(mo => mo.Reason != null)
                    .Select(x => new GetWrongItemsResult
                    {
                        MoveOrderId = x.MoveOrder.Id,
                        ItemCode = x.ItemCode,
                        ItemDescription = x.Item.ItemDescription,
                        Uom = x.Item.Uom.UomDescription,
                        Quantity = x.Quantity,
                        ProductionDate = x.ProductionDate,
                        Reason = x.Reason
                    }).OrderByDescending(x => x.MoveOrderId);



                return await PagedList<GetWrongItemsResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
