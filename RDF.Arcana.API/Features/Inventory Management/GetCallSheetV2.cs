using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
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
                public List<int> TransactionItemIds { get; set; }
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
                    public int TransactionItemId { get; set; }
                    public int BbdId { get; set; }
                    public decimal Quantity { get; set; }
                    public DateTime BbdDate { get; set; }
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
                var now = DateTime.Now;

                var transactionsQuery = _context.Transactions
                    .Where(t => t.CreatedAt >= request.DateFrom
                                && t.CreatedAt < adjustedDateTo
                                && t.Status != Status.Cancelled)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(t => t.TransactionItems).ThenInclude(i => i.Item)
                    .Include(t => t.TransactionItems).ThenInclude(i => i.TransactionItemBbd)
                    .Include(t => t.Client)
                    .OrderByDescending(t => t.CreatedAt);

                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy, cancellationToken);

                if (user?.UserRoles?.UserRoleName == Roles.Cdo)
                {
                    transactionsQuery = transactionsQuery
                        .Where(t => t.AddedBy == request.AccessBy)
                        .OrderByDescending(t => t.CreatedAt);
                }

                //search
                if (!string.IsNullOrEmpty(request.Search))
                {
                    transactionsQuery = transactionsQuery
                        .Where(t => t.Client.BusinessName.Contains(request.Search)
                                    || t.Client.Fullname.Contains(request.Search))
                        .OrderByDescending(t => t.CreatedAt);
                }

                var transactionsList = await transactionsQuery.ToListAsync(cancellationToken);

                var allItems = await _context.Items
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

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

                        var graySum = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd < now && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var redSum = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd >= now && bbd.Bbd <= now.AddDays(10) && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var orangeSum = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd > now.AddDays(10) && bbd.Bbd <= now.AddDays(15) && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var greenSum = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd > now.AddDays(15) && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var allTransactionItems = g
                            .SelectMany(t => t.TransactionItems)
                            .ToList();

                        var callSheetDtos = allItems
                            .Select(item =>
                            {
                                var matchingTIs = allTransactionItems
                                    .Where(ti => ti.Item.ItemCode == item.ItemCode)
                                    .ToList();

                                var salesIn = matchingTIs.Sum(x => x.Quantity);

                                var remainingInv = matchingTIs.Sum(x => x.RemainingQuantity);

                                var allMatchedBbds = matchingTIs
                                    .SelectMany(ti => ti.TransactionItemBbd)
                                    .Where(bbd => bbd.IsActive)
                                    .ToList();

                                var bbdDtos = allMatchedBbds
                                    .Select(bbd => new GetCallSheetV2Result.TransactionItemDto.BbdDto
                                    {

                                        TransactionItemId = bbd.TransactionItemsId,
                                        BbdId = bbd.Id,
                                        Quantity = bbd.Quantity,
                                        BbdDate = bbd.Bbd,
                                        RemainingQuantity = bbd.RemainingQuantity
                                    })
                                    .ToList();

                                return new GetCallSheetV2Result.TransactionItemDto
                                {
                                    TransactionItemIds = matchingTIs
                                        .Select(ti => ti.Id)
                                        .Distinct()
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

                return await Task.FromResult(pagedList);
            }
        }
    }
}
