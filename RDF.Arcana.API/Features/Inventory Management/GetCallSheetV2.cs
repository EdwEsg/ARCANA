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
    [Route("api/get-callsheet-v2"), ApiController]
    public class GetCallSheetV2 : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetCallSheetV2(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCallSheetV2Query query)
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;
            }

            try
            {
                var to = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    to.CurrentPage,
                    to.PageSize,
                    to.TotalCount,
                    to.TotalPages,
                    to.HasNextPage,
                    to.HasPreviousPage);
                var result = new
                {
                    to,
                    to.CurrentPage,
                    to.PageSize,
                    to.TotalCount,
                    to.TotalPages,
                    to.HasNextPage,
                    to.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetCallSheetV2Query : UserParams, IRequest<PagedList<GetCallSheetV2Result>>
        {
            public string Search { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetCallSheetV2Result
        {
            public DateTime ModifiedDate { get; set; }
            public int ClientId { get; set; }
            public string CustomerName { get; set; }
            public string BusinessName { get; set; }
            public DateTime CallSheetDate { get; set; }
            public int Gray { get; set; }
            public int Red { get; set; }
            public int Orange { get; set; }
            public int Green { get; set; }
            public List<TransactionItemDto> CallSheetDtos { get; set; }
            public class TransactionItemDto
            {
                public List<TransactionItemInfoDto> TransactionItemIds { get; set; }
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public decimal SalesIn { get; set; }
                public decimal RemainingInv { get; set; }
                public decimal EndingInv { get; set; }
                public decimal SalesOut { get; set; }
                public decimal SuggestedPo { get; set; }
                public decimal AverageSales { get; set; }
                public List<BbdDto> bbdDtos { get; set; }
                public class BbdDto
                {
                    //public int TransactionId { get; set; }
                    public int BbdId { get; set; }
                    //public decimal Quantity { get; set; }
                    public DateTime BbdDate { get; set; }
                    public decimal RemainingQuantity { get; set; }
                }
                public class TransactionItemInfoDto
                {
                    public int TransactionId { get; set; }
                    public decimal Quantity { get; set; }
                    public decimal RemainingQuantity { get; set; }
                }

            }

        }

        public class Handler : IRequestHandler<GetCallSheetV2Query, PagedList<GetCallSheetV2Result>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetCallSheetV2Result>> Handle(GetCallSheetV2Query request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);
                var now = new DateTime(2025, 3, 12);

                // Build a base query for transactions
                IQueryable<Transactions> baseTxQuery = _context.Transactions
                    .AsNoTracking()
                    .Where(t =>
                        t.CreatedAt >= request.DateFrom &&
                        t.CreatedAt < adjustedDateTo &&
                        t.Status != Status.Cancelled
                    );

                // If user is Cdo, filter
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                if (user?.UserRoles?.UserRoleName == Roles.Cdo)
                {
                    baseTxQuery = baseTxQuery.Where(t => t.AddedBy == request.AccessBy);
                }

                // Search if needed
                if (!string.IsNullOrEmpty(request.Search))
                {
                    baseTxQuery = baseTxQuery.Where(t =>
                        t.Client.BusinessName.Contains(request.Search) ||
                        t.Client.Fullname.Contains(request.Search)
                    );
                }

                // Instead of heavy .Include, project minimal data
                // We only need the TransactionItems + Client (for grouping) + some columns
                var txList = await baseTxQuery
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => new
                    {
                        TransactionId = t.Id,
                        t.ClientId,
                        ClientFullName = t.Client.Fullname,
                        ClientBusinessName = t.Client.BusinessName,
                        t.CreatedAt,
                        t.AddedBy,
                        Items = t.TransactionItems
                            .Select(txItem => new
                            {
                                TransactionItem = txItem,
                                ItemCode = txItem.Item.ItemCode,
                                ItemDescription = txItem.Item.ItemDescription,
                                BbdRows = txItem.TransactionItemBbd
                            })
                    })
                    .ToListAsync(cancellationToken);

                // "Re-hydrate" minimal "Transaction" objects
                var transactionsList = txList.Select(x =>
                {
                    var trans = new Transactions
                    {
                        Id = x.TransactionId,
                        ClientId = x.ClientId,
                        CreatedAt = x.CreatedAt,
                        AddedBy = x.AddedBy,
                        Client = new RDF.Arcana.API.Domain.Clients
                        {
                            Id = x.ClientId,
                            Fullname = x.ClientFullName,
                            BusinessName = x.ClientBusinessName
                        },
                        TransactionItems = x.Items.Select(i =>
                        {
                            var txItem = i.TransactionItem;
                            // We'll reattach the "Item" only with code + desc
                            txItem.Item = new Items
                            {
                                ItemCode = i.ItemCode,
                                ItemDescription = i.ItemDescription
                            };
                            // Reattach the BBD
                            txItem.TransactionItemBbd = i.BbdRows.ToList();
                            return txItem;
                        }).ToList()
                    };
                    return trans;
                }).ToList();

                // Now do your grouping + sums
                var groupedData = transactionsList
                    .GroupBy(t => new
                    {
                        t.ClientId,
                        CustomerName = t.Client.Fullname,
                        BusinessName = t.Client.BusinessName
                    })
                    .Select(g =>
                    {
                        var callSheetDate = g.Max(t => t.CreatedAt);

                        var itemCodesForGroup = g
                            .SelectMany(t => t.TransactionItems)
                            .Select(ti => ti.Item.ItemCode)
                            .Distinct()
                            .ToList();

                        var allBbdForGroup = _context.TransactionItemBbd
                            .AsNoTracking()
                            .Where(b => b.IsActive && itemCodesForGroup.Contains(b.ItemCode))
                            .ToList();

                        var graySum = allBbdForGroup
                            .Where(bbd => bbd.Bbd < now)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                        var redSum = allBbdForGroup
                            .Where(bbd => bbd.Bbd >= now && bbd.Bbd <= now.AddDays(10))
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                        var orangeSum = allBbdForGroup
                            .Where(bbd => bbd.Bbd > now.AddDays(10) && bbd.Bbd <= now.AddDays(15))
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                        var greenSum = allBbdForGroup
                            .Where(bbd => bbd.Bbd > now.AddDays(15))
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                        var allTransactionItems = g
                            .SelectMany(t => t.TransactionItems)
                            .Where(t => t.RemainingQuantity > 0)
                            .ToList();

                        // We'll fetch all "Items" once
                        var allItems = _context.Items
                            .AsNoTracking()
                            .ToList();

                        var callSheetDtos = allItems
                            .Select(item =>
                            {
                                var matchingTIs = allTransactionItems
                                    .Where(ti => ti.Item.ItemCode == item.ItemCode)
                                    .ToList();

                                var salesIn = matchingTIs.Sum(x => x.Quantity);
                                var remainingInv = matchingTIs.Sum(x => x.RemainingQuantity);

                                var itemBbds = _context.TransactionItemBbd
                                    .AsNoTracking()
                                    .Where(b => b.ItemCode == item.ItemCode && b.IsActive && b.RemainingQuantity > 0)
                                    .ToList();

                                var bbdDtos = itemBbds
                                    .Select(bbd => new GetCallSheetV2Result.TransactionItemDto.BbdDto
                                    {
                                        BbdId = bbd.Id,
                                        BbdDate = bbd.Bbd,
                                        RemainingQuantity = bbd.RemainingQuantity
                                    })
                                    .ToList();

                                return new GetCallSheetV2Result.TransactionItemDto
                                {
                                    TransactionItemIds = matchingTIs
                                        .GroupBy(ti => ti.Id)
                                        .Select(grp => grp.First())
                                        .Select(ti => new GetCallSheetV2Result.TransactionItemDto.TransactionItemInfoDto
                                        {
                                            TransactionId = ti.TransactionId,
                                            Quantity = ti.Quantity,
                                            RemainingQuantity = ti.RemainingQuantity
                                        })
                                        .ToList(),
                                    ItemCode = item.ItemCode,
                                    ItemDescription = item.ItemDescription,
                                    SalesIn = salesIn,
                                    RemainingInv = remainingInv,
                                    EndingInv = 0,
                                    SalesOut = 0,
                                    SuggestedPo = 0,
                                    AverageSales = 0,
                                    bbdDtos = bbdDtos
                                };
                            })
                            .ToList();

                        return new GetCallSheetV2Result
                        {
                            ModifiedDate = g
                                .SelectMany(t => t.TransactionItems)
                                .SelectMany(ti => ti.TransactionItemBbd)
                                .Where(b => b.TransactionItems.AddedBy == request.AccessBy)
                                .Select(b => b.CreatedDate)
                                .DefaultIfEmpty(DateTime.MinValue)
                                .Max(),
                            ClientId = g.Key.ClientId,
                            CustomerName = g.Key.CustomerName,
                            BusinessName = g.Key.BusinessName,
                            CallSheetDate = callSheetDate,
                            Gray = graySum,
                            Red = redSum,
                            Orange = orangeSum,
                            Green = greenSum,
                            CallSheetDtos = callSheetDtos
                        };
                    })
                    .OrderByDescending(r => r.CallSheetDate)
                    .ThenBy(r => r.ClientId)
                    .ToList();

                // Pagination
                var totalCount = groupedData.Count;
                var skip = (request.PageNumber - 1) * request.PageSize;
                var items = groupedData
                    .Skip(skip)
                    .Take(request.PageSize)
                    .ToList();

                var pagedList = new PagedList<GetCallSheetV2Result>(
                    items,
                    totalCount,
                    request.PageNumber,
                    request.PageSize
                );

                return pagedList;
            }
        }
    }
}
