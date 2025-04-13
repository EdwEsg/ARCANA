using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-misc-out"), ApiController]
    public class GetMiscOut : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetMiscOut(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetMisOutQuery query)
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

        public class GetMisOutQuery : UserParams, IRequest<PagedList<GetMiscOutResult>>
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? MiscOutId { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetMiscOutResult
        {
            public int Id { get; set; }
            public int? ExternalMoveOrderId { get; set; }
            public string CustomerName { get; set; }
            public string Cluster { get; set; }
            public DateTime? TransactionDate { get; set; }
            public DateTime? DateReceived { get; set; }
            public List<MisceOutItemsDto> MisceOutItems { get; set; }
            public class MisceOutItemsDto
            {
                public int MiscOutId { get; set; }
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public string Uom { get; set; }
                public int Quantity { get; set; }
                public string Reason { get; set; }
                public string Bbd { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetMisOutQuery, PagedList<GetMiscOutResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetMiscOutResult>> Handle(GetMisOutQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var miscOut = _context.MiscellaneousOuts
                    .Include(moi => moi.MiscellaneousOutItems)
                        .ThenInclude(moi => moi.Item)
                            .ThenInclude(i => i.Uom)
                    .AsQueryable();

                var moveOrder = _context.MoveOrders.AsQueryable();

                if (request.AccessBy != 1)
                {
                    miscOut = miscOut.Where(mo => mo.CreatedById == request.AccessBy);
                }

                if (request.MiscOutId == null)
                {
                    miscOut = miscOut.Where(m => m.CreatedDate >= request.DateFrom &&  m.CreatedDate < adjustedDateTo);
                }

                if (request.MiscOutId != null)
                {
                    miscOut = miscOut.Where(m => m.Id == request.MiscOutId);
                }

                var result = miscOut
                    .Select(mo => new GetMiscOutResult
                    {
                        Id = mo.Id,
                        ExternalMoveOrderId = mo.ExternalMoveOrderId,

                        CustomerName = _context.MoveOrders
                        .Where(x => x.MoveOrderIdExternal == mo.ExternalMoveOrderId)
                        .Select(x => x.CustomerName)
                        .FirstOrDefault(),

                        //Ongoing Test for multiclusters
                        Cluster = "Cluster 1",

                        TransactionDate = _context.MoveOrders
                        .Where(x => x.MoveOrderIdExternal == mo.ExternalMoveOrderId)
                        .Select(x => x.TransactionDate)
                        .FirstOrDefault(),

                        DateReceived = _context.MoveOrders
                        .Where(x => x.MoveOrderIdExternal == mo.ExternalMoveOrderId)
                        .Select(x => x.CreatedDate)
                        .FirstOrDefault(),

                        MisceOutItems = mo.MiscellaneousOutItems
                        .Select(moi => new GetMiscOutResult.MisceOutItemsDto
                        {
                            MiscOutId = moi.MiscellaneousOutId,
                            ItemCode = moi.Item.ItemCode,
                            ItemDescription = moi.Item.ItemDescription,
                            Uom = moi.Item.Uom.UomDescription,
                            Quantity = (int)moi.Quantity,
                            Reason = moi.Reason,
                            Bbd = moi.Bbd
                        })
                        .ToList()
                    }).OrderByDescending(x => x.DateReceived);

                return await PagedList<GetMiscOutResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
