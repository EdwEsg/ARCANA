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
            public int? ClientId { get; set; }
            public int? TransactionId { get; set; }
            public int? TransactionItemId { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetCallSheetV2Result
        {
            public int ClientId { get; set; }
            public string CustomerName { get; set; }
            public string BusinessName { get; set; }
            public DateTime CallSheetDate { get; set; }
            public int TotalGray { get; set; }
            public int TotalRed { get; set; }
            public int TotalOrange { get; set; }
            public int TotalGreen { get; set; }
            public List<TransactionDto> CallSheetDtos { get; set; }
            public class TransactionDto
            {
                public int TransactionId { get; set; }
                public int Gray { get; set; }
                public int Red { get; set; }
                public int Orange { get; set; }
                public int Green { get; set; }
                public List<TransactionItemDto> transactionItemDtos { get; set; }
                public class TransactionItemDto
                {
                    public int TransactionItemId { get; set; }
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
                        public decimal Quantity { get; set; }
                        public DateTime BbdDate { get; set; }
                        public decimal RemainingQuantity { get; set; }
                    }
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

                if (!string.IsNullOrEmpty(request.Search))
                {
                    transactionsQuery = transactionsQuery
                        .Where(t => t.Client.BusinessName.Contains(request.Search)
                                    || t.Client.Fullname.Contains(request.Search))
                        .OrderByDescending(t => t.CreatedAt);
                }

                if (request.ClientId.HasValue)
                {
                    transactionsQuery = transactionsQuery
                        .Where(t => t.ClientId == request.ClientId)
                        .OrderByDescending(t => t.CreatedAt);
                }

                if (request.TransactionId.HasValue)
                {
                    transactionsQuery = transactionsQuery
                        .Where(t => t.Id == request.TransactionId)
                        .OrderByDescending(t => t.CreatedAt);
                }

                if (request.TransactionItemId.HasValue)
                {
                    transactionsQuery = transactionsQuery
                        .Where(t => t.TransactionItems.Any(ti => ti.Id == request.TransactionItemId))
                        .OrderByDescending(t => t.CreatedAt);
                }

                var transactionsList = await transactionsQuery.ToListAsync(cancellationToken);

                var groupedData = transactionsList
                    .GroupBy(t => new
                    {
                        t.ClientId,
                        CustomerName = t.Client.Fullname,
                        BusinessName = t.Client.BusinessName,
                        CallSheetDate = t.CreatedAt.Date
                    })
                    .Select(g =>
                    {
                        // Summaries for the entire group
                        var totalGray = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd < now && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var totalRed = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd >= now && bbd.Bbd <= now.AddDays(10) && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var totalOrange = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd > now.AddDays(10) && bbd.Bbd <= now.AddDays(15) && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var totalGreen = g.Sum(t => t.TransactionItems
                            .SelectMany(ti => ti.TransactionItemBbd)
                            .Where(bbd => bbd.Bbd > now.AddDays(15) && bbd.IsActive)
                            .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0);

                        var callSheetDtos = g.Select(t =>
                        {
                            var gray = t.TransactionItems
                                .SelectMany(ti => ti.TransactionItemBbd)
                                .Where(bbd => bbd.Bbd < now && bbd.IsActive)
                                .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                            var red = t.TransactionItems
                                .SelectMany(ti => ti.TransactionItemBbd)
                                .Where(bbd => bbd.Bbd >= now && bbd.Bbd <= now.AddDays(10) && bbd.IsActive)
                                .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                            var orange = t.TransactionItems
                                .SelectMany(ti => ti.TransactionItemBbd)
                                .Where(bbd => bbd.Bbd > now.AddDays(10) && bbd.Bbd <= now.AddDays(15) && bbd.IsActive)
                                .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                            var green = t.TransactionItems
                                .SelectMany(ti => ti.TransactionItemBbd)
                                .Where(bbd => bbd.Bbd > now.AddDays(15) && bbd.IsActive)
                                .Sum(bbd => (int?)bbd.RemainingQuantity) ?? 0;

                            var transactionItemDtos = t.TransactionItems
                                .Select(ti => new GetCallSheetV2Result.TransactionDto.TransactionItemDto
                                {
                                    TransactionItemId = ti.Id,
                                    ItemCode = ti.Item.ItemCode,
                                    ItemDescription = ti.Item.ItemDescription,
                                    SalesIn = ti.Quantity,
                                    RemainingInv = ti.RemainingQuantity,

                                    EndingInv = 0,
                                    SalesOut = 0,
                                    SuggestedPo = 0,
                                    AverageSales = 0,

                                    bbdDtos = ti.TransactionItemBbd
                                        .Where(x => x.IsActive)
                                        .Select(bbd => new GetCallSheetV2Result.TransactionDto.TransactionItemDto.BbdDto
                                        {
                                            BbdId = bbd.Id,
                                            Quantity = bbd.Quantity,
                                            BbdDate = bbd.Bbd,
                                            RemainingQuantity = bbd.RemainingQuantity
                                        })
                                        .ToList()
                                })
                                .ToList();

                            return new GetCallSheetV2Result.TransactionDto
                            {
                                TransactionId = t.Id,
                                Gray = gray,
                                Red = red,
                                Orange = orange,
                                Green = green,
                                transactionItemDtos = transactionItemDtos
                            };
                        }).ToList();

                        return new GetCallSheetV2Result
                        {
                            ClientId = g.Key.ClientId,
                            CustomerName = g.Key.CustomerName,
                            BusinessName = g.Key.BusinessName,
                            CallSheetDate = g.Key.CallSheetDate,
                            TotalGray = totalGray,
                            TotalRed = totalRed,
                            TotalOrange = totalOrange,
                            TotalGreen = totalGreen,
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
