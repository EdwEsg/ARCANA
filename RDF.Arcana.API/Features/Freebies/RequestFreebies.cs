using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class RequestFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("RequestFreebies/{id}")]
    public async Task<IActionResult> Add(RequestFreebiesCommand command, [FromRoute] int id)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            command.ClientId = id;

            var freebies = await _mediator.Send(command);
            if (freebies.IsFailure)
            {
                return BadRequest(freebies);
            }
            return Ok(freebies);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class RequestFreebiesCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public List<Freebie> Freebies { get; set; }
        public int AddedBy { get; set; }

        public class Freebie
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class RequestFreebiesResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string EmailAddress { get; set; }
        public string StoreType { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int AddedBy { get; set; }
        public IEnumerable<FreebieCollection> Freebies { get; set; }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class FreebieCollection
        {
            public int FreebieRequestId { get; set; }
            public string Status { get; set; }
            public int TransactionNumber { get; set; }
            public ICollection<FreebieItemForDirectClient> FreebieItems { get; set; }
        }

        public class FreebieItemForDirectClient
        {
            public int? Id { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string UOM { get; set; }
            public int? Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<RequestFreebiesCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RequestFreebiesCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _context.Clients
                             .Include(storeType => storeType.StoreType)
                             .Include(x => x.OwnersAddress)
                             .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken) ??
                         throw new ClientIsNotFound(request.ClientId);

            var clientFreebies = new List<RequestFreebiesResult.FreebieItemForDirectClient>();

            var freebieResult = new List<RequestFreebiesResult.FreebieCollection>();
            var previousRequestCount =
                await _context.FreebieRequests.CountAsync(f => f.ClientId == request.ClientId && f.Status != Status.Rejected,
                    cancellationToken);

            var withRecentRequest = await _context.FreebieRequests.FirstOrDefaultAsync(
                x => x.ClientId == request.ClientId &&
                     (x.Status == Status.ForReleasing || x.Status == Status.ApproverApproval),
                cancellationToken);


            if (withRecentRequest != null)
            {
                return FreebieErrors.WithRecentRequest(withRecentRequest.Status);
            }

            var isFirstRequest = previousRequestCount == 0;

            var status = isFirstRequest ? Status.ForReleasing : Status.UnderReview;

            if (request.Freebies.Count > 5)
            {
                return FreebieErrors.Exceed5Items();
            }

            if (request.Freebies.Select(x => x.ItemId).Distinct().Count() != request.Freebies.Count)
            {
               return FreebieErrors.CannotBeRepeated();
            }

            foreach (var item in request.Freebies)
            {
                var existingRequest = await _context.FreebieItems
                    .Include(x => x.Items)
                    .Include(f => f.FreebieRequest)
                    .Where(f => f.ItemId == item.ItemId && f.FreebieRequest.ClientId == request.ClientId &&
                                f.FreebieRequest.Status != Status.Rejected)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingRequest != null)
                {
                    return FreebieErrors.AlreadyRequested(existingRequest.Items.ItemDescription);
                }
            }

            var freebieRequest = new FreebieRequest
            {
                ClientId = request.ClientId,
                Status = status,
                IsDelivered = false,
                RequestedBy = request.AddedBy
            };
            _context.FreebieRequests.Add(freebieRequest);

            

            if (isFirstRequest == false)
            {
                var approvers = await _context.Approvers
                 .Include(user => user.User)
                 .Where(x => x.ModuleName == Modules.FreebiesApproval)
                 .OrderBy(x => x.Level)
                 .ToListAsync(cancellationToken);

                if (!approvers.Any())
                {
                    return ApprovalErrors.NoApproversFound(Modules.FreebiesApproval);
                }

                var newRequest = new Request(
                    Modules.FreebiesApproval,
                    request.AddedBy,
                    approvers.First().UserId,
                    approvers.FirstOrDefault(x => x.Level == 2)?.UserId,
                    Status.UnderReview
                );

                await _context.Requests.AddAsync(newRequest, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                foreach (var newRequestApprover in approvers.Select(approver => new RequestApprovers
                {
                    ApproverId = approver.UserId,
                    RequestId = newRequest.Id,
                    Level = approver.Level,
                }))
                {
                    await _context.RequestApprovers.AddAsync(newRequestApprover);
                }
                freebieRequest.RequestId = newRequest.Id;
            }

            //Inventory
            foreach (var freebie in request.Freebies)
            {
                int quantityToSubtract = freebie.Quantity;

                var dbItem = await _context.Items
                    .Include(u => u.Uom)
                    .Where(i => i.Id == freebie.ItemId)
                    .FirstOrDefaultAsync(cancellationToken);

                var totalAvailable = 0;

                totalAvailable += (int)await _context.TransferOrderItems
                    .Where(t =>
                        t.RemainingQuantity > 0 &&
                        t.TransferOrder.CreatedById == request.AddedBy &&
                        t.ItemCode == dbItem.ItemCode
                    )
                    .SumAsync(t => t.RemainingQuantity ?? 0, cancellationToken);

                totalAvailable += (int)await _context.ReturnOrderItems
                    .Where(r =>
                        r.RemainingQuantity > 0 &&
                        r.ReturnOrder.CreatedbyId == request.AddedBy &&
                        r.Item.ItemCode == dbItem.ItemCode
                    )
                    .SumAsync(r => r.RemainingQuantity, cancellationToken);

                totalAvailable += (int)await _context.MoveOrderItems
                    .Where(mo =>
                        mo.RemainingQuantity > 0 &&
                        mo.MoveOrder.CreatedById == request.AddedBy &&
                        mo.ItemCode == dbItem.ItemCode
                    )
                    .SumAsync(mo => mo.RemainingQuantity ?? 0, cancellationToken);

                if (totalAvailable < quantityToSubtract)
                {
                    return FreebieErrors.AlreadyRequested(
                        $"Not enough FIFO stock for {dbItem.ItemDescription} (needed {quantityToSubtract}, only {totalAvailable} available)."
                    );
                }

                var transferStocks = await _context.TransferOrderItems
                    .Where(t =>
                        t.RemainingQuantity > 0 &&
                        t.TransferOrder.CreatedById == request.AddedBy &&
                        t.ItemCode == dbItem.ItemCode
                    )
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken);

                foreach (var tr in transferStocks)
                {
                    if (quantityToSubtract <= 0) break;

                    var canTake = (int)Math.Min((decimal)quantityToSubtract, tr.RemainingQuantity ?? 0);
                    tr.RemainingQuantity -= canTake;
                    quantityToSubtract -= canTake;
                }

                if (quantityToSubtract > 0)
                {
                    var returnStocks = await _context.ReturnOrderItems
                        .Where(r =>
                            r.RemainingQuantity > 0 &&
                            r.ReturnOrder.CreatedbyId == request.AddedBy &&
                            r.Item.ItemCode == dbItem.ItemCode
                        )
                        .OrderBy(r => r.Id)
                        .ToListAsync(cancellationToken);

                    foreach (var ro in returnStocks)
                    {
                        if (quantityToSubtract <= 0) break;

                        var canTake = (int)Math.Min((decimal)quantityToSubtract, ro.RemainingQuantity);
                        ro.RemainingQuantity -= canTake;
                        quantityToSubtract -= canTake;
                    }
                }

                if (quantityToSubtract > 0)
                {
                    var moveStocks = await _context.MoveOrderItems
                        .Where(mo =>
                            mo.RemainingQuantity > 0 &&
                            mo.MoveOrder.CreatedById == request.AddedBy &&
                            mo.ItemCode == dbItem.ItemCode
                        )
                        .OrderBy(mo => mo.Id)
                        .ToListAsync(cancellationToken);

                    foreach (var ms in moveStocks)
                    {
                        if (quantityToSubtract <= 0) break;

                        var canTake = (int)Math.Min((decimal)quantityToSubtract, ms.RemainingQuantity ?? 0);
                        ms.RemainingQuantity -= canTake;
                        quantityToSubtract -= canTake;
                    }
                }

                if (quantityToSubtract > 0)
                {
                    return FreebieErrors.AlreadyRequested(
                        $"Not enough FIFO stock for {dbItem.ItemDescription}"
                    );
                }

                var freebieItem = new FreebieItems
                {
                    FreebieRequestId = freebieRequest.Id,
                    ItemId = freebie.ItemId,
                    Quantity = freebie.Quantity
                };
                await _context.FreebieItems.AddAsync(freebieItem, cancellationToken);

                clientFreebies.Add(new RequestFreebiesResult.FreebieItemForDirectClient
                {
                    Id = freebieItem.Id,
                    ItemId = freebieItem.ItemId,
                    ItemCode = dbItem.ItemCode,
                    ItemDescription = dbItem.ItemDescription,
                    UOM = dbItem.Uom.UomCode,
                    Quantity = freebieItem.Quantity
                });
            }

            freebieResult.Add(new RequestFreebiesResult.FreebieCollection
            {
                FreebieRequestId = freebieRequest.Id,
                Status = freebieRequest.Status,
                TransactionNumber = freebieRequest.Id,
                FreebieItems = clientFreebies,
            });

            var notification = new Domain.Notification
            {
                UserId = request.AddedBy,
                Status = Status.ForReleasing
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var result = new RequestFreebiesResult
            {
                Id = client.Id,
                OwnersName = client.Fullname,
                EmailAddress = client.EmailAddress,
                StoreType = client.StoreType.StoreTypeName,
                OwnersAddress = new RequestFreebiesResult.OwnersAddressCollection
                {
                    HouseNumber = client.OwnersAddress.HouseNumber,
                    StreetName = client.OwnersAddress.StreetName,
                    BarangayName = client.OwnersAddress.Barangay,
                    City = client.OwnersAddress.City,
                    Province = client.OwnersAddress.Province
                },
                PhoneNumber = client.PhoneNumber,
                BusinessName = client.BusinessName,
                Freebies = freebieResult,
                AddedBy = client.AddedBy,
            };

            return Result.Success(result);
        }

        public class GetAllItemsInInventoryResult
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal? AvailQuantity { get; set; }
        }
    }
}
                                                                        