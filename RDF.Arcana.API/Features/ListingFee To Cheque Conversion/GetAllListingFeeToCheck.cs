
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.ListingFee_To_Cheque_Conversion
{
    [Route("api/listing-to-check"), ApiController]
    public class GetAllListingFeeToCheck : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllListingFeeToCheck(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllListingFeeToCheckQuery query)
        {
            try
            {
                var transactions = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage);
                var result = new
                {
                    transactions,
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetAllListingFeeToCheckQuery : UserParams, IRequest<PagedList<GetAllListingFeeToCheckResult>>
        {
            public string Search { get; set; }
            public DateTime? DateFrom { get; set; }
            public DateTime? DateTo { get; set; }
        }

        public class GetAllListingFeeToCheckResult
        {
            public string BusinessName { get; set; }
            public string FullName { get; set; }
            public string Bank { get; set; }
            public string ChequeNo { get; set; }
            public DateTime ChequeDate { get; set; }
            public string Voucher { get; set; }
            public decimal Amount { get; set; }
            public string AddedBy { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class Handler : IRequestHandler<GetAllListingFeeToCheckQuery, PagedList<GetAllListingFeeToCheckResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<PagedList<GetAllListingFeeToCheckResult>> Handle(GetAllListingFeeToCheckQuery request, CancellationToken cancellationToken)
            {
                var checks = _context.Cheque
                    .Include(c => c.Client)
                    .Include(u => u.AddedByUser)
                    .AsSplitQuery()
                    .AsNoTracking();

                if(request.DateFrom is not null &&
                   request.DateTo is not null)
                {
                    var dateFrom = request.DateFrom.Value.Date;
                    var dateTo = request.DateTo.Value.Date.AddDays(1).AddTicks(-1);

                    checks = checks.Where(c => c.CreatedDate >= dateFrom &&
                                               c.CreatedDate <= dateTo);
                }

                if (!string.IsNullOrEmpty(request.Search))
                {
                    checks = checks.Where(c => c.Client.Fullname.Contains(request.Search) ||
                        c.Client.BusinessName.Contains(request.Search));
                }

                var result = checks.Select(c => new GetAllListingFeeToCheckResult
                {
                    BusinessName = c.Client.BusinessName,
                    FullName = c.Client.Fullname,
                    Bank = c.Bank,
                    ChequeNo = c.ChequeNo,
                    ChequeDate = c.ChequeDate,
                    Voucher = c.VoucherNo,
                    Amount = c.Amount,
                    AddedBy = c.AddedByUser.Fullname,
                    CreatedDate = c.CreatedDate
                }).OrderBy(c => c.CreatedDate);

                return PagedList<GetAllListingFeeToCheckResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
