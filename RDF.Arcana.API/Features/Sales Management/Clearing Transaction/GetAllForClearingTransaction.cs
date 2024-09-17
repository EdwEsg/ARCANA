using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Common.Helpers;
using System.Security.Claims;

[Route("api/clearing-transaction")]
[ApiController]
public class GetAllForClearingTransaction : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllForClearingTransaction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("pages")]
    public async Task<IActionResult> Get([FromQuery] GetAllForClearingTransactionQuery query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AddedBy = userId;

                var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

            }

            var transactions = await _mediator.Send(query);

            Response.AddPaginationHeader(transactions.CurrentPage, transactions.PageSize, transactions.TotalCount,
                transactions.TotalPages, transactions.HasNextPage, transactions.HasPreviousPage);

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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllForClearingTransactionQuery : UserParams, IRequest<PagedList<GetAllForClearingTransactionResult>>
    {
        public string Search { get; set; }
        public string Status { get; set; }
        public int? ClusterId { get; set; }
        public int AddedBy { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class GetAllForClearingTransactionResult
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentChannel { get; set; }
        public string ReferenceNo { get; set; }
        public decimal TotalPaymentAmount { get; set; }
        public int ClearingId { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public string ATag { get; set; }
        public string BusinessName { get; set; }
        public class Invoice
		{
			public string InvoiceNo { get; set; }
		}

	}


	public class Handler : IRequestHandler<GetAllForClearingTransactionQuery, PagedList<GetAllForClearingTransactionResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<GetAllForClearingTransactionResult>> Handle(GetAllForClearingTransactionQuery request,
            CancellationToken cancellationToken)
        {
            var paymentTransactions = _context.PaymentRecords
                .Include(x => x.PaymentTransactions)
                .ThenInclude(x => x.Transaction)
                .Include(x => x.PaymentTransactions)
                .ThenInclude(x => x.ClearedPayment)
                .Where(x => x.PaymentTransactions.Any(x => x.Status.Contains(request.Status)));

            //filter for Admin/Finanace/GAS/Treasury
            var adminClusterFilter = _context.Users.Find(request.AddedBy);
            if ((adminClusterFilter.UserRolesId == 1 ||
                    adminClusterFilter.UserRolesId == 7 ||
                    adminClusterFilter.UserRolesId == 8 ||
                    adminClusterFilter.UserRolesId == 9 ||
                    adminClusterFilter.UserRolesId == 10)
                    && request.ClusterId is not null)
            {
                paymentTransactions = paymentTransactions.Where(t => t.PaymentTransactions.Any(pt => pt.Transaction.Client.ClusterId == request.ClusterId));

            }

            if (request.PaymentMethod is not null)
            {
                paymentTransactions = paymentTransactions.Where(t => t.PaymentTransactions.Any(pt => pt.PaymentMethod == request.PaymentMethod));
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                paymentTransactions = paymentTransactions
                    .Where(pt => pt.PaymentTransactions.Any(pt => pt.ReferenceNo.Contains(request.Search)));
            }

            var groupedQuery = paymentTransactions
                .SelectMany(x => x.PaymentTransactions)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.PaymentMethod))
            {
                groupedQuery = groupedQuery.Where(pt => pt.PaymentMethod == request.PaymentMethod);
            }

            var groupedResults = groupedQuery
                .GroupBy(pt => new { pt.PaymentMethod, pt.ReferenceNo, pt.BankName, pt.Transaction.Client.BusinessName, pt.ClearedPayment.ATag, pt.ClearedPayment.Reason })
                .Select(g => new GetAllForClearingTransactionResult
                {
                    BusinessName = g.Key.BusinessName,
                    PaymentMethod = g.Key.PaymentMethod,
                    PaymentChannel = g.Key.BankName,
                    ReferenceNo = g.Key.ReferenceNo,
                    TotalPaymentAmount = g.Sum(pt => pt.PaymentAmount),
                    Date = g.Max(pt => pt.DateReceived),
                    ATag = g.Key.ATag,
                    Reason = g.Key.Reason,
                    Invoices = g.Select(x => new GetAllForClearingTransactionResult.Invoice
                    {
                        InvoiceNo = x.Transaction.InvoiceNo
                    }).Distinct().ToList()
                })
                .OrderBy(pt => pt.Date);

            return await PagedList<GetAllForClearingTransactionResult>.CreateAsync(groupedResults, request.PageNumber, request.PageSize);
		}

	}
}