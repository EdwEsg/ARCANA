using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-freebie-order"), ApiController]
    public class GetFreebie : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetFreebie(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetFreebieQuery query)
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

        public class GetFreebieQuery : UserParams, IRequest<PagedList<GetFreebieResult>>
        {
            public string Search { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int AccessBy { get; set; }
            public string TransactionType { get; set; }
        }

        public class GetFreebieResult
        {
            public int  Id { get; set; }
            public string ClientName { get; set; }
            public string BusinessName { get; set; }
            public string TransactionType { get; set; }
            public decimal TotalQuantity { get; set; }
            public DateTime CreatedDate { get; set; }
            public IEnumerable<FreebieOrderItemDto> FreebieOrderItem { get; set; }
            public class FreebieOrderItemDto
            {
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public string Uom { get; set; }
                public decimal? Quantity { get; set; }
                public string Bbd { get; set; }
                public int? FreebieOrderId { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetFreebieQuery, PagedList<GetFreebieResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetFreebieResult>> Handle(GetFreebieQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var freebieOrder = _context.FreebieOrders
                    .Where(fo => fo.TransactionType == request.TransactionType)
                    .AsNoTracking()
                    .AsQueryable();



                freebieOrder = freebieOrder.Where(f => f.CreatedDate >= request.DateFrom && f.CreatedDate < adjustedDateTo);

                if (!string.IsNullOrEmpty(request.Search))
                {
                    freebieOrder = freebieOrder.Where(f =>
                     f.Client.Fullname.Contains(request.Search) ||
                     f.Client.BusinessName.Contains(request.Search));
                }

                var result = freebieOrder
                    .Select(f => new GetFreebieResult
                    {
                        Id = f.Id,
                        ClientName = f.Client.Fullname,
                        BusinessName = f.Client.BusinessName,
                        TransactionType = f.TransactionType,
                        TotalQuantity = f.TotalQuantity,
                        CreatedDate = f.CreatedDate,
                        FreebieOrderItem = f.FreebieOrderItems.Select(x => new GetFreebieResult.FreebieOrderItemDto
                        {
                            ItemCode = x.Item.ItemCode,
                            ItemDescription = x.Item.ItemDescription,
                            Uom = x.Item.Uom.UomDescription,
                            Quantity = x.Quantity,
                            Bbd = x.Bbd,
                            FreebieOrderId = x.FreebieOrderId
                        })
                    }).OrderByDescending(x => x.CreatedDate);

                return await PagedList<GetFreebieResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
