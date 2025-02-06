using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;
using System.Transactions;
using static RDF.Arcana.API.Features.Inventory_Management.GetFreebie;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-callsheet"), ApiController]
    public class GetCallSheet : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetCallSheet(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCallSheetQuery query)
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

        public class GetCallSheetQuery : UserParams, IRequest<PagedList<GetCallSheetResult>>
        {
            public string Search { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? TransactionId { get; set; }
            public int? TransactionItemId { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetCallSheetResult
        {
            public int TransactionId { get; set; }
            public string CustomerName { get; set; }
            public string BusinessName { get; set; }
            public DateTime CallSheetDate { get; set; }
            public int Gray { get; set; }
            public int Red { get; set; }
            public int Orange { get; set; }
            public int Green { get; set; }
            public List<TransactionItemsDto> TransactionItemsDtos { get; set; }
            public class TransactionItemsDto
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
                public List<BbbDto> BbbDtos { get; set; }
                public class BbbDto
                {
                    public int BbdId { get; set; }
                    public decimal Quantity { get; set; }
                    public DateTime Bbd { get; set; }
                }
            }
        }

        public class GetCallSheetHandler : IRequestHandler<GetCallSheetQuery, PagedList<GetCallSheetResult>>
        {
            private readonly ArcanaDbContext _context;
            public GetCallSheetHandler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetCallSheetResult>> Handle(GetCallSheetQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);
                var now = DateTime.Now;

                var transactions = _context.Transactions
                    .Where(t => t.CreatedAt >= request.DateFrom && t.CreatedAt < adjustedDateTo &&
                        //t.CreatedAt > DateTime.Parse("2025-01-22") &&
                        t.Status != Status.Cancelled)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(t => t.TransactionItems)
                        .ThenInclude(i => i.Item)
                    .Include(t => t.TransactionItems)
                        .ThenInclude(bbd => bbd.TransactionItemBbd)
                    .Include(t => t.Client)
                    .OrderByDescending(t => t.CreatedAt);

                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.Id == request.AccessBy);

                if (user.UserRoles.UserRoleName == Roles.Cdo)
                {
                    transactions = transactions
                        .Where(t => t.AddedBy == request.AccessBy)
                        .OrderByDescending(t => t.CreatedAt);
                }

                if (!string.IsNullOrEmpty(request.Search))
                {
                    transactions = transactions
                        .Where(t => t.Client.BusinessName.Contains(request.Search) ||
                            t.Client.Fullname.Contains(request.Search))
                            .OrderByDescending(t => t.CreatedAt);
                }

                if (request.TransactionId != null)
                {
                    var transaction = await transactions
                        .FirstOrDefaultAsync(t => t.Id == request.TransactionId); 

                    transactions = _context.Transactions.Where(t => t.Id == request.TransactionId)
                        .OrderByDescending(t => t.CreatedAt);
                }

                if (request.TransactionItemId != null)
                {
                    transactions = transactions
                        .Where(t => t.TransactionItems.Any(ti => ti.Id == request.TransactionItemId))
                        .OrderByDescending(t => t.CreatedAt); 
                }


                var result = transactions.Select(t => new GetCallSheetResult
                {
                    TransactionId = t.Id,
                    CustomerName = t.Client.Fullname,
                    BusinessName = t.Client.BusinessName,
                    CallSheetDate = t.CreatedAt,

                    Gray = t.TransactionItems
                    .SelectMany(ti => ti.TransactionItemBbd)
                    .Where(bbd => bbd.Bbd < now)
                    .Sum(bbd => (int?)bbd.Quantity) ?? 0,

                    Red = t.TransactionItems
                    .SelectMany(ti => ti.TransactionItemBbd)
                    .Where(bbd => bbd.Bbd >= now && bbd.Bbd <= now.AddDays(10))
                    .Sum(bbd => (int?)bbd.Quantity) ?? 0,

                    Orange = t.TransactionItems
                    .SelectMany(ti => ti.TransactionItemBbd)
                    .Where(bbd => bbd.Bbd > now.AddDays(10) && bbd.Bbd <= now.AddDays(15))
                    .Sum(bbd => (int?)bbd.Quantity) ?? 0,

                    Green = t.TransactionItems
                    .SelectMany(ti => ti.TransactionItemBbd)
                    .Where(bbd => bbd.Bbd > now.AddDays(15))
                    .Sum(bbd => (int?)bbd.Quantity) ?? 0,
                    TransactionItemsDtos = t.TransactionItems.Select(ti => new GetCallSheetResult.TransactionItemsDto
                    {
                        TransactionItemId = ti.Id,
                        ItemCode = ti.Item.ItemCode,
                        ItemDescription = ti.Item.ItemDescription,
                        SalesIn = ti.Quantity,
                        RemainingInv = ti.RemainingQuantity,
                        BbbDtos = ti.TransactionItemBbd.Select(bbd => new GetCallSheetResult.TransactionItemsDto.BbbDto
                        {
                            BbdId = bbd.Id,
                            Quantity = bbd.Quantity,
                            Bbd = bbd.Bbd
                        }).ToList()
                    }).ToList()
                });

                return await PagedList<GetCallSheetResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
