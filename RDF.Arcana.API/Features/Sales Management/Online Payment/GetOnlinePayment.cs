
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment;

[Route("api/OnlinePayments"), ApiController]
public class GetOnlinePayment : ControllerBase
{
    private readonly IMediator _mediator;
    public GetOnlinePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> GetOnlinePayments([FromQuery] GetOnlinePaymentQuery query)
    {
        try
        {
            var online = await _mediator.Send(query);

            Response.AddPaginationHeader(
                online.CurrentPage,
                online.PageSize,
                online.TotalCount,
                online.TotalPages,
                online.HasPreviousPage,
                online.HasNextPage
                );

            var result = new
            {
                online,
                online.CurrentPage,
                online.PageSize,
                online.TotalCount,
                online.TotalPages,
                online.HasPreviousPage,
                online.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    public class GetOnlinePaymentQuery : UserParams, IRequest<PagedList<GetOnlinePaymentResult>>
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetOnlinePaymentResult 
    {
        public int Id { get; set; }
        public string OnlinePlatform { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set;}
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class Handler : IRequestHandler<GetOnlinePaymentQuery, PagedList<GetOnlinePaymentResult>>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<GetOnlinePaymentResult>> Handle(GetOnlinePaymentQuery request, CancellationToken cancellationToken)
        {
            IQueryable<OnlinePayments> onlinePayments = _context.OnlinePayments
                .Include(u => u.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                onlinePayments = onlinePayments.Where(o => o.OnlinePlatform.Contains(request.Search));
            }

            if (request.IsActive is not null)
            {
                onlinePayments = onlinePayments.Where(o => o.IsActive == request.IsActive);
            }

            var result = onlinePayments.Select(onlinePayments => new GetOnlinePaymentResult
            {
                Id = onlinePayments.Id,
                OnlinePlatform = onlinePayments.OnlinePlatform,
                CreatedAt = onlinePayments.CreatedAt.ToString("MMM d, yyyy"),
                UpdatedAt = onlinePayments.UpdatedAt.HasValue
                            ? onlinePayments.UpdatedAt.Value.ToString("MMM d, yyyy")
                            : null,

                IsActive = onlinePayments.IsActive,
                AddedBy = onlinePayments.AddedByUser.Fullname,
                ModifiedBy = onlinePayments.ModifiedBy.HasValue
                             ? _context.Users.FirstOrDefault(u => u.Id == onlinePayments.ModifiedBy.Value).Fullname
                             : null
            });

            return await PagedList<GetOnlinePaymentResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}
