﻿using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Domain.New_Doamin;
using RDF.Arcana.API.Features.Clients.Prospecting.Approved;
using RDF.Arcana.API.Features.Clients.Prospecting.Rejected;
using RDF.Arcana.API.Features.Clients.Prospecting.Request;

namespace RDF.Arcana.API.Features.Clients.Prospecting;

public static class ProspectingMappingProfiles
{
    public static GetAllRequestedProspectAsync.GetAllRequestedProspectResult
        ToGetGetAllProspectResult(this Approvals requestedClient)
    {
        return new GetAllRequestedProspectAsync.GetAllRequestedProspectResult
        {
            Id = requestedClient.ClientId,
            OwnersName = requestedClient.Client.Fullname,
            // CreatedAt = requestedClient.,
            BusinessName = requestedClient.Client.BusinessName,
            PhoneNumber = requestedClient.Client.PhoneNumber,
            CustomerType = requestedClient.Client.CustomerType,
            AddedBy = requestedClient.Client.AddedBy,
            Address = requestedClient.Client.Address,
            IsActive = requestedClient.IsActive
        };
    }
    
    public static GetAllApprovedProspectAsync.GetAllApprovedProspectResult
        ToGetGetAllApprovedProspectResult(this Approvals approvedClient)
    {
        return new GetAllApprovedProspectAsync.GetAllApprovedProspectResult
        {
            Id = approvedClient.ClientId,
            OwnersName = approvedClient.Client.Fullname,
            // CreatedAt = approvedClient.DateApproved,
            BusinessName = approvedClient.Client.BusinessName,
            PhoneNumber = approvedClient.Client.PhoneNumber,
            CustomerType = approvedClient.Client.CustomerType,
            AddedBy = approvedClient.Client.AddedBy,
            Address = approvedClient.Client.Address,
            IsActive = approvedClient.IsActive
        };
    }
    
    public static GetAllRejectProspectAsync.GetAllRejectProspectResult
        ToGetGetAllRejectProspectResult(this Approvals rejectClient)
    {
        return new GetAllRejectProspectAsync.GetAllRejectProspectResult
        {
            Id = rejectClient.ClientId,
            OwnersName = rejectClient.Client.Fullname,
            // CreatedAt = rejectClient.DateRejected,
            BusinessName = rejectClient.Client.BusinessName,
            PhoneNumber = rejectClient.Client.PhoneNumber,
            CustomerType = rejectClient.Client.CustomerType,
            AddedBy = rejectClient.Client.AddedBy,
            Address = rejectClient.Client.Address,
            IsActive = rejectClient.IsActive,
            Reason = rejectClient.Reason
        };
    }
}