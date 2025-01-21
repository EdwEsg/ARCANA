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
            public decimal? ReturnByClient { get; set; }
            public decimal? Soh { get; set; }
            public decimal? Sales { get; set; }
        }

        public class FreebieGroup
        {
            public string ItemCode { get; set; }
            public decimal Quantity { get; set; }
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
                if (request.AccessBy == 25) // Depot-specific logic
                {
                    var moReturnByCdo = _context.MoveOrderItems
                        .Where(mo => mo.Reason != null)
                        .GroupBy(x => new { x.ItemCode })
                        .Select(x => new
                        {
                            ItemCode = x.Key.ItemCode,
                            Quantity = x.Sum(x => x.Quantity)
                        });

                    var groupTransferIn = _context.TransferOrders
                        .Where(to => to.Status == Status.Received && to.TransferToId == request.AccessBy)
                        .SelectMany(to => to.TransferOrderItems)
                        .GroupBy(toi => toi.ItemCode)
                        .Select(g => new
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(x => x.Quantity)
                        });

                    var consolidateDepotGroups = _context.Items
                        .Where(i => string.IsNullOrEmpty(request.Search) ||
                                    i.ItemCode.Contains(request.Search) ||
                                    i.ItemDescription.Contains(request.Search))
                        .Select(i => new GetInventoryMrpResult
                        {
                            ItemCode = i.ItemCode,
                            ItemDescription = i.ItemDescription,
                            TransferIn = groupTransferIn
                                .Where(ti => ti.ItemCode == i.ItemCode)
                                .Select(ti => ti.Quantity)
                                .FirstOrDefault() ?? 0,
                            Soh = (moReturnByCdo
                                .Where(to => to.ItemCode == i.ItemCode)
                                .Select(to => to.Quantity)
                                .FirstOrDefault()) +
                                  (groupTransferIn
                                .Where(ti => ti.ItemCode == i.ItemCode)
                                .Select(ti => ti.Quantity)
                                .FirstOrDefault() ?? 0)
                        })
                        .OrderBy(x => x.ItemCode);

                    return await PagedList<GetInventoryMrpResult>.CreateAsync(consolidateDepotGroups, request.PageNumber, request.PageSize);
                }


                else
                {

                    var groupReceiving = _context.MoveOrderItems
                        .Where(mo => mo.CreatedBy.Id == request.AccessBy)
                        .GroupBy(x => new { x.ItemCode })
                        .Select(x => new
                        {
                            ItemCode = x.Key.ItemCode,
                            Quantity = x.Sum(x => x.Quantity),
                            RemainingQuantity = x.Sum(x => x.RemainingQuantity),
                            ActualQuantity = x.Sum(x => x.ActualQuantity)
                        });

                    //Freebies-----------------------------------------------------------------

                    var groupFreebieInventory = _context.FreebieOrderItems
                        .Where(f => f.FreebieOrder.CreatedById == request.AccessBy &&
                                    f.FreebieOrder.TransactionType == Status.Freebie)
                        .GroupBy(f => f.Item.ItemCode)
                        .Select(g => new FreebieGroup
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(g => g.Quantity),
                        });

                    var groupFreebieRegistration = _context.FreebieItems
                        .Where(f => f.FreebieRequest.RequestedBy == request.AccessBy &&
                            f.FreebieRequest.Status == Status.Released)
                        .GroupBy(f => f.Items.ItemCode)
                        .Select(g => new FreebieGroup
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(g => g.Quantity),
                        });

                    var groupOfFreebies = groupFreebieInventory
                        .Concat(groupFreebieRegistration)
                        .GroupBy(f => f.ItemCode)
                        .Select(g => new FreebieGroup
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(f => f.Quantity),
                        });

                    //-------------------------------------------------------------------

                    var groupSales = _context.TransactionItems
                        .Where(t => t.CreatedAt > DateTime.Parse("2025-01-22"))
                        .GroupBy(t => t.Item.ItemCode)
                        .Select(g => new
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(t => t.Quantity)
                        });

                    var groupSampling = _context.FreebieOrderItems
                        .Where(f => f.FreebieOrder.CreatedById == request.AccessBy &&
                                    f.FreebieOrder.TransactionType == Status.Sampling)
                        .GroupBy(f => f.Item.ItemCode)
                        .Select(g => new
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(g => g.Quantity),
                        });

                    var groupTransferOut = _context.TransferOrders
                        .Where(to => (to.Status == Status.Received && to.CreatedById == request.AccessBy) ||
                                     (to.Status == Status.ForReceiving && to.CreatedById == request.AccessBy))
                        .SelectMany(to => to.TransferOrderItems)
                        .GroupBy(toi => toi.ItemCode)
                        .Select(g => new
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(x => x.Quantity)
                        });

                    var groupTransferIn = _context.TransferOrders
                        .Where(to => to.Status == Status.Received &&
                                     to.TransferToId == request.AccessBy)
                        .SelectMany(to => to.TransferOrderItems)
                        .GroupBy(toi => toi.ItemCode)
                        .Select(g => new
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(x => x.Quantity),
                            RemainingQuantity = g.Sum(x => x.RemainingQuantity)
                        });

                    var groupReturn = _context.ReturnOrderItems
                        .Where(r => r.ReturnOrder.CreatedbyId == request.AccessBy)
                        .GroupBy(i => i.Item.ItemCode)
                        .Select(g => new
                        {
                            ItemCode = g.Key,
                            Quantity = g.Sum(x => x.Quantity)
                        });

                    var groupReplace = _context.ReplaceOrderItems
                        .Where(r => r.ReturnOrder.CreatedbyId == request.AccessBy)
                        .GroupBy(i => i.Item.ItemCode)
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

                            Sales = groupSales
                                .Where(s => s.ItemCode == i.ItemCode)
                                .Select(s => s.Quantity)
                                .FirstOrDefault(),

                            Receiving = groupReceiving
                                .Where(r => r.ItemCode == i.ItemCode)
                                .Select(r => r.ActualQuantity)
                                .FirstOrDefault() ?? 0,

                            TransferIn = groupTransferIn
                                .Where(ti => ti.ItemCode == i.ItemCode)
                                .Select(ti => ti.Quantity)
                                .FirstOrDefault() ?? 0,

                            TransferOut = groupTransferOut
                                .Where(to => to.ItemCode == i.ItemCode)
                                .Select(to => to.Quantity)
                                .FirstOrDefault() ?? 0,

                            Freebie = groupOfFreebies
                                .Where(f => f.ItemCode == i.ItemCode)
                                .Select(f => f.Quantity)
                                .FirstOrDefault(),

                            Sampling = groupSampling
                                .Where(f => f.ItemCode == i.ItemCode)
                                .Select(f => f.Quantity)
                                .FirstOrDefault(),

                            ReturnByClient = groupReturn
                                .Where(ti => ti.ItemCode == i.ItemCode)
                                .Select(ti => ti.Quantity)
                                .FirstOrDefault(),

                            Replace = groupReplace
                                .Where(ti => ti.ItemCode == i.ItemCode)
                                .Select(ti => ti.Quantity)
                                .FirstOrDefault(),

                            Soh = Math.Max(
                                ((groupReceiving
                                    .Where(r => r.ItemCode == i.ItemCode)
                                    .Select(r => r.RemainingQuantity)
                                    .FirstOrDefault() ?? 0)
                                 +
                                 (groupTransferIn
                                    .Where(ti => ti.ItemCode == i.ItemCode)
                                    .Select(ti => ti.RemainingQuantity)
                                    .FirstOrDefault() ?? 0)
                                 +
                                 (groupReturn
                                    .Where(ti => ti.ItemCode == i.ItemCode)
                                    .Select(ti => ti.Quantity)
                                    .FirstOrDefault())
                                ), 0)
                        })
                        .OrderBy(x => x.ItemCode);

                    return await PagedList<GetInventoryMrpResult>.CreateAsync(
                        consolidateGroups,
                        request.PageNumber,
                        request.PageSize);
                }
                

            }
        }
    }
}
