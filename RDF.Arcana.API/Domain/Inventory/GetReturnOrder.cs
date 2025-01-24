
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Domain.Inventory
{
    [Route("api/get-return-order"), ApiController]
    public class GetReturnOrder : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetReturnOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetReturnOrderQuery query)
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

        public class GetReturnOrderQuery : UserParams, IRequest<PagedList<GetReturnOrderResult>>
        {
            public string Search { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ReturnId { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetReturnOrderResult
        {
            public int ReturnId { get; set; }
            public int ClientId { get; set; }
            public string ClientName { get; set; }
            public string BusinessName { get; set; }
            public DateTime CreatedDate { get; set; }
            public decimal TotalReturnPrice { get; set; }
            public decimal TotalExchangePrice { get; set; }
            public IEnumerable<ReturnOrderItemsDto> ReturnItems { get; set; }
            public IEnumerable<ReplaceOrderItemsDto> ReplaceItems { get; set; }
            public class ReturnOrderItemsDto
            {
                public int ItemId { get; set; }
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public decimal Net { get; set; }
                public string Bbd { get; set; }
                public string Reason { get; set; }
            }
            public class ReplaceOrderItemsDto
            {
                public int ItemId { get; set; }
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public decimal Net { get; set; }
                public string Bbd { get; set; }
                public string Reason { get; set; }
            }

        }

        public class Handler : IRequestHandler<GetReturnOrderQuery, PagedList<GetReturnOrderResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetReturnOrderResult>> Handle(GetReturnOrderQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var returnOrder = _context.ReturnedOrders
                    .AsNoTracking()
                    .AsQueryable();

                returnOrder = returnOrder.Where(r => r.CreatedDate >= request.DateFrom && r.CreatedDate < adjustedDateTo);

                if (request.ReturnId != null)
                {
                    returnOrder = returnOrder.Where(r => r.Id == request.ReturnId);
                }

                if (!string.IsNullOrEmpty(request.Search))
                {
                    returnOrder = returnOrder.Where(r =>
                        r.Client.Fullname.Contains(request.Search) ||
                        r.Client.BusinessName.Contains(request.Search));
                }

                var result = returnOrder
                    .Select(r => new GetReturnOrderResult
                    {
                        ReturnId = r.Id,
                        ClientId = r.Client.Id,
                        ClientName = r.Client.Fullname,
                        BusinessName = r.Client.BusinessName,
                        CreatedDate = r.CreatedDate,
                        TotalReturnPrice = r.TotalReturn,
                        TotalExchangePrice = r.TotalExchange,
                        ReturnItems = r.ReturnOrderItems.Select(x => new GetReturnOrderResult.ReturnOrderItemsDto
                        {
                            ItemId = x.Item.Id,
                            ItemCode = x.Item.ItemCode,
                            ItemDescription = x.Item.ItemDescription,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            Net = x.Price * x.Quantity,
                            Bbd = x.Bbd,
                            Reason = x.Reason
                        }),
                        ReplaceItems = r.ReplaceOrderItems.Select(x => new GetReturnOrderResult.ReplaceOrderItemsDto
                        {
                            ItemId = x.Item.Id,
                            ItemCode = x.Item.ItemCode,
                            ItemDescription = x.Item.ItemDescription,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            Net = x.Price * x.Quantity,
                            Bbd = x.Bbd,
                            Reason = x.Reason
                        })
                    }).OrderByDescending(x => x.CreatedDate);

                return await PagedList<GetReturnOrderResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
