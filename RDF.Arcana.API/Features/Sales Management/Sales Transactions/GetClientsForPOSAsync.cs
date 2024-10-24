﻿using System.Security.Claims;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Transactions;

[Route("api/clients"), ApiController]
public class GetClientsForPOSAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetClientsForPOSAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("pos")]
    public async Task<IActionResult> Get([FromQuery]GetClientsForPOSAsyncQuery query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
               && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;

                var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                if (roleClaim != null)
                {
                    query.RoleName = roleClaim.Value;
                }
            }
            var result = await _mediator.Send(query);
            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    public record GetClientsForPOSAsyncQuery : IRequest<Result>
    {
        public string Search { get; set; }
        public int AccessBy { get; set; }
        public string RoleName { get; set; }
    }

    public class GetClientsForPOSAsyncResult
    {
        public int ClientId { get; set; }
        public string OwnersName { get; set; }
        public string BusinessName { get; set; }
        public int? PriceModeId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool? VariableDiscount { get; set; }
        public decimal? SpecialDiscount { get; set; }
        public int? SpecialDiscountId { get; set; }
        public bool? IsOnetimeSp { get; set; }
        public int? CreditLimit { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

    }

    public class Handler : IRequestHandler<GetClientsForPOSAsyncQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetClientsForPOSAsyncQuery request, CancellationToken cancellationToken)
        {
            var userClusters = await _context.CdoClusters.FirstOrDefaultAsync(x => x.UserId == request.AccessBy, cancellationToken);

            List<GetClientsForPOSAsyncResult> clients = new();

            if (userClusters != null)
            {
                clients = await _context.Clients
                    .Include(fd => fd.FixedDiscounts)
                    .Include(sp => sp.SpecialDiscounts)
                    .Include(to => to.Term)
                    .Include(t => t.Transactions)
                    .Include(a => a.BusinessAddress)
                    .Where(x => x.RegistrationStatus == Status.Approved &&
                                x.ClusterId == userClusters.ClusterId)
                    //.Where(x => x.Transactions.Any(ts => ts.Status != Status.Pending))
                    .Select(cl => new GetClientsForPOSAsyncResult
                    {
                        ClientId = cl.Id,
                        BusinessName = cl.BusinessName,
                        OwnersName = cl.Fullname,
                        PriceModeId = cl.PriceModeId,
                        VariableDiscount = cl.VariableDiscount,
                        DiscountPercentage = cl.FixedDiscounts.DiscountPercentage,
                        SpecialDiscount = cl.SpecialDiscounts.First(x => x.IsActive && x.Status == Status.Approved).Discount,
                        SpecialDiscountId = cl.SpecialDiscounts.First(x => x.IsActive && x.Status == Status.Approved).Id,
                        IsOnetimeSp = cl.SpecialDiscounts.First(x => x.IsActive && x.Status == Status.Approved).IsOneTime,
                        CreditLimit = cl.Term.CreditLimit,
                        City = cl.BusinessAddress.City,
                        Province = cl.BusinessAddress.Province
                    })
                    .ToListAsync(cancellationToken: cancellationToken);

                if (request.Search is not null)
                {
                    clients = clients.Where(cl =>
                        cl.OwnersName.Contains(request.Search) ||
                        cl.BusinessName.Contains(request.Search)).ToList();
                }
            }


            if (request.RoleName.Contains(Roles.Admin) || request.RoleName.ToLower().Contains(Roles.Finanace))
            {               
                clients = await _context.Clients
                    .Include(to => to.Term)
                    .Include(t => t.Transactions)
                    .Include(a => a.BusinessAddress)
                    .Where(x => x.RegistrationStatus == Status.Approved &&
                                (x.Term.TermsId != 2 || (x.Transactions.Any(ts => ts.Status == Status.Paid) ||
                                x.Transactions.Count == 0)))
                    //.Where(c => c.RegistrationStatus == Status.Approved) //this is for temporary restraining the 1 up 1 down Logic
                    .Select(cl => new GetClientsForPOSAsyncResult
                    {
                        ClientId = cl.Id,
                        BusinessName = cl.BusinessName,
                        OwnersName = cl.Fullname,
                        PriceModeId = cl.PriceModeId,
                        VariableDiscount = cl.VariableDiscount,
                        DiscountPercentage = cl.FixedDiscounts.DiscountPercentage,
                        SpecialDiscount = cl.SpecialDiscounts.First(x => x.IsActive && x.Status == Status.Approved).Discount,
                        SpecialDiscountId = cl.SpecialDiscounts.First(x => x.IsActive && x.Status == Status.Approved).Id,
                        IsOnetimeSp = cl.SpecialDiscounts.First(x => x.IsActive && x.Status == Status.Approved).IsOneTime,
                        CreditLimit = cl.Term.CreditLimit,
                        City = cl.BusinessAddress.City,
                        Province = cl.BusinessAddress.Province
                    })
                    .ToListAsync(cancellationToken: cancellationToken);

                if (request.Search is not null)
                {
                    clients = clients.Where(cl =>
                        cl.OwnersName.Contains(request.Search) ||
                        cl.BusinessName.Contains(request.Search)).ToList();
                }
            }
           
            return Result.Success(clients);


        }
    }
}
