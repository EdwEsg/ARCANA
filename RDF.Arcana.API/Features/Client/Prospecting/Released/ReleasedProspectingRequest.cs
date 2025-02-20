using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;
using RDF.Arcana.API.Features.Freebies;

namespace RDF.Arcana.API.Features.Client.Prospecting.Released;

[Route("api/Prospecting")]
[ApiController]
public class ReleasedProspectingRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public ReleasedProspectingRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ReleasedProspectingRequest/{id:int}")]
    public async Task<IActionResult> ReleasedProspecting([FromForm] ReleasedProspectingRequestCommand command,
        [FromRoute] int id)
    {
        try
        {
            command.FreebieRequestId = id;

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class ReleasedProspectingRequestCommand : IRequest<Result>
    {
        public int FreebieRequestId { get; set; }
        public IFormFile PhotoProof { get; set; }
        public IFormFile ESignature { get; set; }
    }

    public class Handler : IRequestHandler<ReleasedProspectingRequestCommand, Result>
    {
        private readonly Cloudinary _cloudinary;
        private readonly ArcanaDbContext _context;

        public Handler(IOptions<CloudinaryOptions> options, ArcanaDbContext context)
        {
            var account = new Account(
                options.Value.Cloudname,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
            _context = context;
        }

        public async Task<Result> Handle(ReleasedProspectingRequestCommand request, CancellationToken cancellationToken)
        {
            var validateClientRequest = await _context.FreebieRequests
                .Include(fr => fr.Clients)
                .Include(fr => fr.FreebieItems)
                    .ThenInclude(fi => fi.Items)
                    .ThenInclude(i => i.Uom)
                .FirstOrDefaultAsync(fr =>
                    fr.Id == request.FreebieRequestId &&
                    fr.IsDelivered == false &&
                    fr.Status == Status.ForReleasing,
                    cancellationToken);

            if (validateClientRequest == null)
            {
                return ClientErrors.NotFound();
            }

            if ((request.PhotoProof?.Length ?? 0) > 0 || (request.ESignature?.Length ?? 0) > 0)
            {
                var photoProofUrl = string.Empty;
                var eSignatureUrl = string.Empty;

                if (request.PhotoProof?.Length > 0)
                {
                    await using var proofStream = request.PhotoProof.OpenReadStream();

                    var photoProofParams = new ImageUploadParams
                    {
                        File = new FileDescription(request.PhotoProof.FileName, proofStream),
                        PublicId = $"{WebUtility.UrlEncode(validateClientRequest.Clients.BusinessName)}/" +
                                   $"{request.PhotoProof.FileName}"
                    };

                    var photoProofUploadResult = await _cloudinary.UploadAsync(photoProofParams, cancellationToken);
                    if (photoProofUploadResult.Error != null)
                    {
                        throw new Exception(photoProofUploadResult.Error.Message);
                    }
                    photoProofUrl = photoProofUploadResult.SecureUrl.ToString();
                }

                if (request.ESignature?.Length > 0)
                {
                    await using var signatureStream = request.ESignature.OpenReadStream();

                    var eSignatureParams = new ImageUploadParams
                    {
                        File = new FileDescription(request.ESignature.FileName, signatureStream),
                        PublicId = $"{WebUtility.UrlEncode(validateClientRequest.Clients.BusinessName)}/" +
                                   $"{request.ESignature.FileName}"
                    };

                    var eSignatureUploadResult = await _cloudinary.UploadAsync(eSignatureParams, cancellationToken);
                    if (eSignatureUploadResult.Error != null)
                    {
                        throw new Exception(eSignatureUploadResult.Error.Message);
                    }
                    eSignatureUrl = eSignatureUploadResult.SecureUrl.ToString();
                }

                foreach (var fItem in validateClientRequest.FreebieItems)
                {
                    var dbItem = fItem.Items;
                    var quantityNeeded = fItem.Quantity;

                    var totalAvailable = 0;

                    totalAvailable += (int)await _context.TransferOrderItems
                        .Where(t =>
                            t.RemainingQuantity > 0 &&
                            t.TransferOrder.CreatedById == validateClientRequest.RequestedBy &&
                            t.ItemCode == dbItem.ItemCode && 
                            t.TransferOrder.Status == Status.Received)
                        .SumAsync(t => t.RemainingQuantity ?? 0, cancellationToken);

                    totalAvailable += (int)await _context.ReturnOrderItems
                        .Where(r =>
                            r.RemainingQuantity > 0 &&
                            r.ReturnOrder.CreatedbyId == validateClientRequest.RequestedBy &&
                            r.Item.ItemCode == dbItem.ItemCode)
                        .SumAsync(r => r.RemainingQuantity, cancellationToken);

                    totalAvailable += (int)await _context.MoveOrderItems
                        .Where(mo =>
                            mo.RemainingQuantity > 0 &&
                            mo.MoveOrder.CreatedById == validateClientRequest.RequestedBy &&
                            mo.ItemCode == dbItem.ItemCode)
                        .SumAsync(mo => mo.RemainingQuantity ?? 0, cancellationToken);

                    if (totalAvailable < quantityNeeded)
                    {
                        return FreebieErrors.AlreadyRequested(
                            $"Not enough FIFO stock for {dbItem.ItemDescription}. " +
                            $"Needed {quantityNeeded}, only {totalAvailable} available."
                        );
                    }

                    var transferStocks = await _context.TransferOrderItems
                        .Where(t =>
                            t.RemainingQuantity > 0 &&
                            t.TransferOrder.CreatedById == validateClientRequest.RequestedBy &&
                            t.ItemCode == dbItem.ItemCode &&
                            t.TransferOrder.Status == Status.Received)
                        .OrderBy(t => t.Id)
                        .ToListAsync(cancellationToken);

                    foreach (var tr in transferStocks)
                    {
                        if (quantityNeeded <= 0) break;

                        decimal? remainingDecimal = tr.RemainingQuantity; 
                        int canTake = (int)Math.Min((decimal)quantityNeeded, remainingDecimal ?? 0m); 
                        tr.RemainingQuantity = (tr.RemainingQuantity ?? 0m) - canTake;              
                        quantityNeeded -= canTake;
                    }

                    if (quantityNeeded > 0)
                    {
                        var returnStocks = await _context.ReturnOrderItems
                            .Where(r =>
                                r.RemainingQuantity > 0 &&
                                r.ReturnOrder.CreatedbyId == validateClientRequest.RequestedBy &&
                                r.Item.ItemCode == dbItem.ItemCode)
                            .OrderBy(r => r.Id)
                            .ToListAsync(cancellationToken);

                        foreach (var ro in returnStocks)
                        {
                            if (quantityNeeded <= 0) break;

                            decimal? remainingDecimal = ro.RemainingQuantity; 
                            int canTake = (int)Math.Min((decimal)quantityNeeded, remainingDecimal ?? 0m); 
                            ro.RemainingQuantity = (ro.RemainingQuantity) - canTake;                    
                            quantityNeeded -= canTake;
                        }
                    }

                    if (quantityNeeded > 0)
                    {
                        var moveStocks = await _context.MoveOrderItems
                            .Where(mo =>
                                mo.RemainingQuantity > 0 &&
                                mo.MoveOrder.CreatedById == validateClientRequest.RequestedBy &&
                                mo.ItemCode == dbItem.ItemCode)
                            .OrderBy(mo => mo.Id)
                            .ToListAsync(cancellationToken);

                        foreach (var ms in moveStocks)
                        {
                            if (quantityNeeded <= 0) break;

                            decimal? remainingDecimal = ms.RemainingQuantity;
                            int canTake = (int)Math.Min((decimal)quantityNeeded, remainingDecimal ?? 0m); 
                            ms.RemainingQuantity = (ms.RemainingQuantity ?? 0m) - canTake;              
                            quantityNeeded -= canTake;
                        }
                    }

                    if (quantityNeeded > 0)
                    {
                        return FreebieErrors.AlreadyRequested(
                            $"Not enough FIFO stock for {dbItem.ItemDescription} after partial allocation."
                        );
                    }
                }

                validateClientRequest.Status = Status.Released;
                validateClientRequest.IsDelivered = true;
                validateClientRequest.PhotoProofPath = photoProofUrl;
                validateClientRequest.ESignaturePath = eSignatureUrl;

                var notification = new Domain.Notification
                {
                    UserId = validateClientRequest.RequestedBy,
                    Status = Status.Released
                };
                await _context.Notifications.AddAsync(notification, cancellationToken);
            }

            validateClientRequest.Clients.RegistrationStatus = Status.PendingRegistration;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}