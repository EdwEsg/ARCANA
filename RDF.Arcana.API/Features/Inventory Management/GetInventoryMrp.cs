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
            public string Search { get; set; }
        }

        public class GetInventoryMrpResult
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal? Receiving { get; set; }
            public decimal? TransferIn { get; set; }
            public decimal? TransferOut { get; set; }
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

                //Add --moveOrders
                var groupReceiving = _context.MoveOrderItems
                    .Where(mo => mo.Reason == null && mo.CreatedBy.Id == request.AccessBy)
                    .GroupBy(x => new
                    {
                        x.ItemCode
                    })
                    .Select(x => new
                    {
                        ItemCode = x.Key.ItemCode,
                        Quantity = x.Sum(x => x.ActualQuantity)
                    });


                //Subtract
                var groupTransferOut = _context.TransferOrders
                    .Where(to => to.Status == Status.Received &&
                        to.CreatedById == request.AccessBy)
                    .SelectMany(to => to.TransferOrderItems)
                    .GroupBy(toi => toi.ItemCode)
                    .Select(g => new
                    {
                        ItemCode = g.Key,
                        Quantity = g.Sum(x => x.Quantity)
                    });


                //Add
                var groupTransferIn = _context.TransferOrders
                    .Where(to => to.Status == Status.Received &&
                        to.TransferToId == request.AccessBy)
                    .SelectMany(to => to.TransferOrderItems)
                    .GroupBy(toi => toi.ItemCode)
                    .Select(g => new
                    {
                        ItemCode = g.Key,
                        Quantity = g.Sum(x => x.Quantity)
                    });


                var consolidateGroups = _context.Items
                        .Where(i => string.IsNullOrEmpty(request.Search) || 
                            i.ItemCode.Contains(request.Search) ||  
                            i.ItemDescription.Contains(request.Search))
                    .Select(i => new GetInventoryMrpResult
                    {
                        ItemCode = i.ItemCode,
                        ItemDescription = i.ItemDescription,
                        Receiving = groupReceiving
                            .Where(r => r.ItemCode == i.ItemCode)
                            .Select(r => r.Quantity)
                            .FirstOrDefault() ?? 0,
                        TransferIn = groupTransferIn
                            .Where(ti => ti.ItemCode == i.ItemCode)
                            .Select(ti => ti.Quantity)
                            .FirstOrDefault() ?? 0,
                        TransferOut = groupTransferOut
                            .Where(to => to.ItemCode == i.ItemCode)
                            .Select(to => to.Quantity)
                            .FirstOrDefault() ?? 0,
                        Freebie = 0,
                        Sampling = 0,
                        Issue = 0,
                        Replace = 0,
                        Return = 0,
                        Soh = ((groupReceiving
                            .Where(r => r.ItemCode == i.ItemCode)
                            .Select(r => r.Quantity)
                            .FirstOrDefault() ?? 0) +
                            (groupTransferIn
                            .Where(ti => ti.ItemCode == i.ItemCode)
                            .Select(ti => ti.Quantity)
                            .FirstOrDefault() ?? 0)) -
                            (groupTransferOut
                            .Where(to => to.ItemCode == i.ItemCode)
                            .Select(to => to.Quantity)
                            .FirstOrDefault() ?? 0)
                    })
                    .OrderBy(x => x.ItemCode);




                return await PagedList<GetInventoryMrpResult>.CreateAsync(consolidateGroups, request.PageNumber, request.PageSize);

            }
        }
    }
}
