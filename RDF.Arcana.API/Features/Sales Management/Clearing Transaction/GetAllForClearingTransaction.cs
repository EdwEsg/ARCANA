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
        public string OwnersName { get; set; }
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
        public async Task<PagedList<GetAllForClearingTransactionResult>> Handle(
    GetAllForClearingTransactionQuery request, CancellationToken cancellationToken)
        {
            var adminClusterFilter = await _context.Users.FindAsync(request.AddedBy);

            var query = _context.PaymentTransactions
                .Where(pt => pt.PaymentRecord.Status == request.Status &&
                             pt.Transaction.Status != Status.Cancelled &&
                             pt.Transaction.Status != Status.Voided &&
                             pt.Status != Status.Voided &&
                             pt.Status != Status.Cancelled &&
                             pt.TotalAmountReceived > 0);

            // Admin, Sir Roger
            if (adminClusterFilter.Id != 1 && adminClusterFilter.Id != 17)
            {
                query = query.Where(pt => pt.PaymentMethod != PaymentMethods.Withholding);
            }

            // Admin, GAS, Audit, Finance, Treasury
            int[] roleIds = { 1, 7, 8, 9, 10 };
            int userRoleId = adminClusterFilter.UserRolesId ?? 0;

            if (roleIds.Contains(userRoleId) && request.ClusterId != null)
            {
                query = query.Where(pt => pt.Transaction.Client.ClusterId == request.ClusterId);
            }

            if (!string.IsNullOrEmpty(request.PaymentMethod))
            {
                query = query.Where(pt => pt.PaymentMethod == request.PaymentMethod);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(pt => pt.ReferenceNo.Contains(request.Search));
            }

            var groupedQuery = query
                .Select(pt => new
                {
                    pt.PaymentMethod,
                    pt.ReferenceNo,
                    pt.BankName,
                    pt.TotalAmountReceived,
                    pt.DateReceived,
                    pt.Id, 
                    BusinessName = pt.Transaction.Client.BusinessName,
                    ATag = pt.ClearedPayment.ATag,
                    Reason = pt.ClearedPayment.Reason,
                    OwnersName = pt.Transaction.Client.Fullname,
                    InvoiceNo = pt.Transaction.InvoiceNo
                })
                .GroupBy(x => new
                {
                    x.PaymentMethod,
                    x.BankName,
                    x.BusinessName,
                    x.ATag,
                    x.Reason,
                    x.OwnersName,
                    ReferenceNoGroupingKey = x.ReferenceNo ?? x.Id.ToString()
                })
                .Select(g => new GetAllForClearingTransactionResult
                {
                    BusinessName = g.Key.BusinessName,
                    OwnersName = g.Key.OwnersName,
                    PaymentMethod = g.Key.PaymentMethod,
                    PaymentChannel = g.Key.BankName,
                    ReferenceNo = g.FirstOrDefault(x => x.ReferenceNo != null).ReferenceNo, 
                    TotalPaymentAmount = g.Sum(x => x.TotalAmountReceived),
                    Date = g.Max(x => x.DateReceived),
                    ATag = g.Key.ATag,
                    Reason = g.Key.Reason,
                    Invoices = g.Select(x => new GetAllForClearingTransactionResult.Invoice
                    {
                        InvoiceNo = x.InvoiceNo
                    }).Distinct().ToList()
                })
                .OrderBy(r => r.Date);

            return await PagedList<GetAllForClearingTransactionResult>.CreateAsync(
                groupedQuery,
                request.PageNumber,
                request.PageSize);
        }



    }
}