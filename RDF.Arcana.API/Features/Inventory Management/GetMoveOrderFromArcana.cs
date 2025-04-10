
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-move-order-arcana"), ApiController]
    public class GetMoveOrderFromArcana : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetMoveOrderFromArcana(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetMoveOrderFromArcanaQuery query)
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
        public class GetMoveOrderFromArcanaQuery : UserParams, IRequest<PagedList<GetMoveOrderFromArcanaResult>>
        {
            public string Search { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? MoveOrderId { get; set; }
            public int AccessBy { get; set; }
            public bool? IsMiscellaneousIn { get; set; }
            public int? MiscInId { get; set; }
        }

        public class GetMoveOrderFromArcanaResult
        {
            public int? MisInId { get; set; }
            public int? MoveOrderId { get; set; }
            public string CustomerName { get; set; }
            public string Cluster { get; set; }
            public DateTime? TransactionDate { get; set; }
            public DateTime? DateReceived { get; set; }
            public string Route { get; set; }
            public string Details { get; set; }
            public string Area { get; set; }
            public IEnumerable<GetMoveItemsDto> MoveItems { get; set; }
            public class GetMoveItemsDto
            {
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public string Uom { get; set; }
                public decimal? ActualQuantity { get; set; }
                public string ProductionDate { get; set; }
                public string Reason { get; set; }
                public decimal? Quantity { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetMoveOrderFromArcanaQuery, PagedList<GetMoveOrderFromArcanaResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetMoveOrderFromArcanaResult>> Handle(GetMoveOrderFromArcanaQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var moveOrders = _context.MoveOrders
                    .AsNoTracking()
                    .Include(moi => moi.MoveOrderItems)
                    .AsQueryable();

                if (request.AccessBy != 1)
                {
                    moveOrders = moveOrders.Where(mo => mo.CreatedById == request.AccessBy);
                }

                if (request.MoveOrderId == null && request.IsMiscellaneousIn is not true)
                {
                    moveOrders = moveOrders.Where(t => (t.CreatedDate >= request.DateFrom && t.CreatedDate < adjustedDateTo) && t.Type == null);
                }


                if (request.MoveOrderId != null && request.IsMiscellaneousIn is not true)
                {
                    moveOrders = moveOrders.Where(x => x.MoveOrderIdExternal == request.MoveOrderId && x.Type == null);
                }

                //is Miscellaneous
                if (request.MoveOrderId == null && request.MiscInId == null && request.IsMiscellaneousIn is true)
                {
                    moveOrders = moveOrders.Where(t => (t.CreatedDate >= request.DateFrom && t.CreatedDate < adjustedDateTo) && t.Type == Status.MiscIn);
                }

                if (request.MiscInId != null && request.IsMiscellaneousIn is true)
                {
                    moveOrders = moveOrders.Where(x => x.Id == request.MiscInId && x.Type == Status.MiscIn);
                }



                if (!string.IsNullOrEmpty(request.Search))
                {
                    moveOrders = moveOrders.Where(mo =>
                        mo.CustomerName.Contains(request.Search) ||
                        mo.MoveOrderIdExternal.ToString().Contains(request.Search));
                }

                var result = moveOrders
                    .Select(mo => new GetMoveOrderFromArcanaResult
                    {
                        MisInId = mo.Id,
                        MoveOrderId = mo.MoveOrderIdExternal,
                        CustomerName = mo.CustomerName,
                        Cluster = mo.CreatedBy.CdoCluster.Cluster.ClusterType,
                        TransactionDate = mo.TransactionDate,
                        DateReceived = mo.CreatedDate,
                        Route = mo.Route,
                        Details = mo.Details,
                        Area = mo.Area,
                        MoveItems = mo.MoveOrderItems.Select(x => new GetMoveOrderFromArcanaResult.GetMoveItemsDto
                        {
                            ItemCode = x.ItemCode,
                            ItemDescription = x.Item.ItemDescription,
                            Uom = x.Uom.UomDescription,
                            ActualQuantity = x.ActualQuantity,
                            ProductionDate = x.ProductionDate,
                            Reason = x.Reason,
                            Quantity = x.Quantity
                            
                        })
                    }).OrderByDescending(x => x.DateReceived);

                return await PagedList<GetMoveOrderFromArcanaResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
