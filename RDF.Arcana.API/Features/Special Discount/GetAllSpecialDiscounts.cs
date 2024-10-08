﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Special_Discount;
[Route("api/special-discount")]

public class GetAllSpecialDiscounts : ControllerBase
{
    private readonly IMediator _mediator;
    public GetAllSpecialDiscounts(IMediator mediator) { _mediator = mediator; }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllSpecialDiscountQuery query)
    {
        try
        {
            
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AddedBy = userId;

                var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                if (roleClaim != null)
                {
                    query.RoleName = roleClaim.Value;
                }
            }

            var specialDiscounts = await _mediator.Send(query);

            Response.AddPaginationHeader(
                specialDiscounts.CurrentPage,
                specialDiscounts.PageSize,
                specialDiscounts.TotalCount,
                specialDiscounts.TotalPages,
                specialDiscounts.HasPreviousPage,
                specialDiscounts.HasNextPage
            );

            var result = new
            {
                specialDiscounts,
                specialDiscounts.CurrentPage,
                specialDiscounts.PageSize,
                specialDiscounts.TotalCount,
                specialDiscounts.TotalPages,
                specialDiscounts.HasPreviousPage,
                specialDiscounts.HasNextPage
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
 
    public class GetAllSpecialDiscountQuery : UserParams, IRequest<PagedList<SpecialDiscountResult>>
    {
        public string Search { get; set; }
        public string SpDiscountStatus { get; set; }
        public string RoleName { get; set; }
        public int AddedBy { get; set; }
        public bool? Status { get; set; }
        public int? ClusterId { get; set; }
    }

    public class SpecialDiscountResult
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RequestId { get; set; }
        public string ClientName { get; set; }
        public string BusinessName { get; set; }
        public decimal Discount { get; set; }
        public bool IsOneTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Requestor { get; set; }
        public string RequestorMobileNumber { get; set; }
        public string CurrentApprover { get; set; }
        public string CurrentApproverPhoneNumber { get; set; }
        public string NextApprover { get; set; }
        public string NextApproverPhoneNumber { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllSpecialDiscountQuery, PagedList<SpecialDiscountResult>>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context) { _context = context; }

        public async Task<PagedList<SpecialDiscountResult>> Handle(GetAllSpecialDiscountQuery request, CancellationToken cancellationToken)
        {
            IQueryable<SpecialDiscount> specialDiscounts = _context.SpecialDiscounts
                .Include(r => r.Request)
                .Include(cl => cl.Client)
                .Include(ad => ad.AddedByUser);


            //filter for Admin/Finanace/GAS/Treasury
            var adminClusterFilter = _context.Users.Find(request.AddedBy);
            if ((adminClusterFilter.UserRolesId == 1 ||
                    adminClusterFilter.UserRolesId == 7 ||
                    adminClusterFilter.UserRolesId == 8 ||
                    adminClusterFilter.UserRolesId == 9 ||
                    adminClusterFilter.UserRolesId == 10)
                    && request.ClusterId is not null)
            {
                specialDiscounts = specialDiscounts.Where(t => t.Client.ClusterId == request.ClusterId);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                specialDiscounts = specialDiscounts.Where(x =>
                    x.Client.Fullname.Contains(request.Search));
            }

            switch (request.RoleName)
            {
                case var roleName when roleName.Contains(Roles.Approver) &&
                (!string.IsNullOrWhiteSpace(request.SpDiscountStatus) &&
                                          request.SpDiscountStatus.ToLower() !=
                                          Status.UnderReview.ToLower()):
                    specialDiscounts = specialDiscounts.Where(sp => sp.Request.Approvals.Any(x =>
                        x.Status == request.SpDiscountStatus && x.ApproverId == request.AddedBy &&
                        x.IsActive == true));
                    break;

                case var roleName when roleName.Contains(Roles.Approver):
                    specialDiscounts = specialDiscounts.Where(lf =>
                        lf.Request.Status == request.SpDiscountStatus && lf.Request.CurrentApproverId == request.AddedBy);
                    break;

                case Roles.Cdo:

                    var userClusters = await _context.CdoClusters.FirstOrDefaultAsync(x => x.UserId == request.AddedBy, cancellationToken);

                        if (request.SpDiscountStatus is Status.Voided)
                        {

                            specialDiscounts = specialDiscounts
                                .Where(x => x.AddedBy == request.AddedBy &&
                                            x.Status == request.SpDiscountStatus);

                            //Get the result

                            var voidedResults = specialDiscounts.Select(sp => new SpecialDiscountResult
                            {
                                Id = sp.Id,
                                ClientId = sp.ClientId,
                                ClientName = sp.Client.Fullname,
                                BusinessName = sp.Client.BusinessName,
                                Requestor = sp.AddedByUser.Fullname,
                                Discount = sp.Discount,
                                IsOneTime = sp.IsOneTime,
                                RequestId = sp.RequestId,
                                UpdatedAt = sp.UpdatedAt,
                                CreatedAt = sp.CreatedAt
                            });

                            voidedResults = voidedResults.OrderBy(r => r.Id);

                            //Return the result
                            return await PagedList<SpecialDiscountResult>.CreateAsync(voidedResults, request.PageNumber, request.PageSize);

                        }
                        
                        if (userClusters is null)
                        {
                            specialDiscounts = specialDiscounts
                            .Where(x => x.Status == request.SpDiscountStatus);
                            break;
                        }

                    specialDiscounts = specialDiscounts.Where(x => 
                        x.Status == request.SpDiscountStatus && 
                        x.Client.ClusterId == userClusters.ClusterId);
                    break;

                case Roles.Admin:
                    specialDiscounts = specialDiscounts.Where(x => x.Status == request.SpDiscountStatus);
                    break;
            }
            
            //Get all the under review request for the Approver
            //It will access the request table where status is Under Review
            //And CurrentApproverId is the Logged In user (Approver)
            if (request.RoleName.Contains(Roles.Approver) && request.SpDiscountStatus == Status.UnderReview)
            {
                specialDiscounts = specialDiscounts.Where(x => x.Request.CurrentApproverId == request.AddedBy);
            }
            
            //Filter based on Status
            if (request.Status != null)
            {
                specialDiscounts = specialDiscounts.Where(x => x.IsActive == request.Status);
            }
            
            var result = specialDiscounts.Select(sp => new SpecialDiscountResult
            {
                Id = sp.Id,
                ClientId = sp.ClientId,
                ClientName = sp.Client.Fullname,
                BusinessName = sp.Client.BusinessName,
                Requestor = sp.AddedByUser.Fullname,
                RequestorMobileNumber = sp.Request.Requestor.MobileNumber,
                CurrentApprover = sp.Request.CurrentApprover.Fullname,
                CurrentApproverPhoneNumber = sp.Request.CurrentApprover.MobileNumber,
                NextApprover =  sp.Request.NextApprover.Fullname,
                NextApproverPhoneNumber = sp.Request.NextApprover.MobileNumber,
                Discount = sp.Discount,
                IsOneTime = sp.IsOneTime,
                RequestId = sp.RequestId,
                UpdatedAt = sp.UpdatedAt,
                CreatedAt = sp.CreatedAt
            });
            
            //Return the result
            return await PagedList<SpecialDiscountResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}
