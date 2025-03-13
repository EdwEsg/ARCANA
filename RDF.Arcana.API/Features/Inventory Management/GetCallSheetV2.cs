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
                    public int BbdId { get; set; }
                    public DateTime? BbdDate { get; set; }
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
                var now = DateTime.Now;

                var transactionsList = await _context.Transactions
                    .AsNoTracking()
                    .Where(t => t.CreatedAt >= request.DateFrom &&
                                t.CreatedAt < adjustedDateTo &&
                                t.Status != Status.Cancelled)
                    .OrderByDescending(t => t.CreatedAt)
                    .Include(t => t.TransactionItems).ThenInclude(i => i.Item)
                    .Include(t => t.TransactionItems).ThenInclude(i => i.TransactionItemBbd)
                    .Include(t => t.Client)
                    .ToListAsync(cancellationToken);

                var allItems = await _context.Items.AsNoTracking().ToListAsync(cancellationToken);

                var allBbdRows = await _context.TransactionItemBbd
                    .AsNoTracking()
                    .Where(b => b.RemainingQuantity > 0)
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

                        var itemCodesForGroup = g.SelectMany(t => t.TransactionItems)
                            .Select(ti => ti.Item.ItemCode)
                            .Distinct()
                            .ToList();

                        var allBbdForGroup = allBbdRows
                            .Where(b =>
                                itemCodesForGroup.Contains(b.ItemCode) &&
                                // b.ClientsId must match the group’s ClientId
                                b.ClientsId == g.Key.ClientId
                            )
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

                        var allTransactionItems = g.SelectMany(t => t.TransactionItems).ToList();

                        var callSheetDtos = allItems.Select(item =>
                        {
                            var matchingTIs = allTransactionItems.Where(ti => ti.Item.ItemCode == item.ItemCode).ToList();
                            var salesIn = matchingTIs.Sum(x => x.Quantity);
                            var remainingInv = matchingTIs.Sum(x => x.RemainingQuantity);

                            var itemBbds = allBbdRows
                                .Where(b =>
                                    b.ItemCode == item.ItemCode &&
                                    b.IsActive &&
                                    b.RemainingQuantity > 0 &&
                                    b.ClientsId == g.Key.ClientId // ensure same client
                                )
                                .ToList();

                            var bbdDtos = itemBbds.Select(bbd => new GetCallSheetV2Result.TransactionItemDto.BbdDto
                            {
                                BbdId = bbd.Id,
                                BbdDate = bbd?.Bbd,
                                RemainingQuantity = bbd.RemainingQuantity
                            }).ToList();

                            var endingInv = bbdDtos.Sum(x => x.RemainingQuantity);
                            var salesOut = salesIn - remainingInv;
                            var suggestedPo = 0m;
                            var averageSales = 0m;

                            return new GetCallSheetV2Result.TransactionItemDto
                            {
                                TransactionItemIds = matchingTIs.GroupBy(ti => ti.Id)
                                    .Select(grp => grp.First())
                                    .Select(ti => new GetCallSheetV2Result.TransactionItemDto.TransactionItemInfoDto
                                    {
                                        TransactionId = ti.TransactionId,
                                        Quantity = ti.Quantity,
                                        RemainingQuantity = ti.RemainingQuantity,
                                    }).ToList(),
                                ItemCode = item.ItemCode,
                                ItemDescription = item.ItemDescription,
                                SalesIn = salesIn,
                                RemainingInv = remainingInv,
                                EndingInv = endingInv,
                                SalesOut = salesOut,
                                SuggestedPo = suggestedPo,
                                AverageSales = averageSales,
                                bbdDtos = bbdDtos
                            };
                        }).ToList();

                        return new GetCallSheetV2Result
                        {
                            ModifiedDate = g.SelectMany(t => t.TransactionItems)
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
                    }).AsQueryable();

                var totalCount = groupedData.Count();
                var pagedData = groupedData
                    .OrderByDescending(r => r.CallSheetDate)
                    .ThenBy(r => r.ClientId)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                return new PagedList<GetCallSheetV2Result>(
                    pagedData,
                    totalCount,
                    request.PageNumber,
                    request.PageSize
                );
            }
        }
    }
}
