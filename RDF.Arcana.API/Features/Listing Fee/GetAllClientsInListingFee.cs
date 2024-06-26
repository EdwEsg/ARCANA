﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Regular;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee")]
[ApiController]
public class GetAllClientsInListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllClientsInListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllClientsInListingFee")]
    public async Task<IActionResult> GetAllClientsInListingFees(
        [FromQuery] GetAllClientsInListingFeeQuery query)
    {
        try
        {

            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AddedBy = userId;
            }

            var regularClient = await _mediator.Send(query);

            Response.AddPaginationHeader(
                regularClient.CurrentPage,
                regularClient.PageSize,
                regularClient.TotalCount,
                regularClient.TotalPages,
                regularClient.HasPreviousPage,
                regularClient.HasNextPage
            );

            var result = new
            {
                regularClient,
                regularClient.CurrentPage,
                regularClient.PageSize,
                regularClient.TotalCount,
                regularClient.TotalPages,
                regularClient.HasPreviousPage,
                regularClient.HasNextPage
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllClientsInListingFeeQuery : UserParams, IRequest<PagedList<GetAllClientsInListingFeeResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public bool? IncludeRejected { get; set; }
        public string StoreType { get; set; }
        public string Origin { get; set; }
        public int AddedBy { get; set; }
    }

    public class GetAllClientsInListingFeeResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string BusinessName { get; set; }
        public IEnumerable<ListingFee> ListingFees { get; set; }

        public class ListingFee
        {
            public int Id { get; set; }
            public int RequestId { get; set; }
            public IEnumerable<ListingItem> ListingItems { get; set; }
        }

        public class ListingItem
        {
            public int Id { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public int Sku { get; set; }
            public decimal UnitCost { get; set; }
        }


    }

    public class Handler : IRequestHandler<GetAllClientsInListingFeeQuery,
        PagedList<GetAllClientsInListingFeeResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllClientsInListingFeeResult>> Handle(GetAllClientsInListingFeeQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Clients> clientsListingFee = _context.Clients
                .Include(rq => rq.Request)
                .ThenInclude(user => user.Requestor)
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.Approvals)
                .ThenInclude(cap => cap.Approver)
                .Include(lf => lf.ListingFees)
                .ThenInclude(li => li.ListingFeeItems)
                .ThenInclude(item => item.Item)
                .ThenInclude(uom => uom.Uom)
                .Where(clients => clients.RegistrationStatus == Status.Approved)
                .AsNoTracking();

            var user = await _context.CdoClusters.FirstOrDefaultAsync(c => c.UserId == request.AddedBy, cancellationToken);
            
            if (!string.IsNullOrEmpty(request.Search))
            {
                clientsListingFee = clientsListingFee.Where(x =>
                    x.BusinessName.Contains(request.Search) ||
                    x.StoreType.StoreTypeName.Contains(request.Search) ||
                    x.Fullname.Contains(request.Search)
                );
            }

            if (request.Origin != null)
            {
                clientsListingFee = clientsListingFee.Where(x => x.Origin == request.Origin);
            }

            if (!string.IsNullOrEmpty(request.StoreType))
            {
                clientsListingFee = clientsListingFee.Where(x => x.StoreType.StoreTypeName == request.StoreType);
            }

            if (request.Status != null)
            {
                clientsListingFee = clientsListingFee.Where(x => x.IsActive == request.Status);
            }

            if (request.IncludeRejected == false)
            {
                clientsListingFee = clientsListingFee.Where(x =>
                    x.RegistrationStatus == Status.Approved ||
                    x.RegistrationStatus == Status.UnderReview ||
                    x.RegistrationStatus == Status.Requested);
            }

            var result = clientsListingFee
                .Where(x => user != null && x.ClusterId == user.ClusterId)
                .Select(x => x.ToGetAllClientsInListingFeeResult());

            return await PagedList<GetAllClientsInListingFeeResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}
