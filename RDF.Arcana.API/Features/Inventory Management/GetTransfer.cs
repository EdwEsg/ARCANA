
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-transfer"), ApiController]
    public class GetTransfer : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetTransfer(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTransferForReceivingQuery query)
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;
            }

            try
            {
                var to = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    to.CurrentPage,
                    to.PageSize,
                    to.TotalCount,
                    to.TotalPages,
                    to.HasNextPage,
                    to.HasPreviousPage);
                var result = new
                {
                    to,
                    to.CurrentPage,
                    to.PageSize,
                    to.TotalCount,
                    to.TotalPages,
                    to.HasNextPage,
                    to.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetTransferForReceivingQuery : UserParams, IRequest<PagedList<GetTransferResult>>
        {
            public string Search { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? TransferOrderId { get; set; }
            public string TransferType { get; set; }
            public string Status { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetTransferResult
        {
            public int Id { get; set; }
            public string CreatedBy { get; set; }            
            public string CreatedByCluster { get; set; }
            public string TransferTo { get; set; }
            public string TransferToCluster { get; set; }
            public string TransactionType { get; set; }
            public decimal TotalQuantity { get; set; }
            public DateTime TransactionDate { get; set; }
            public string TransferType { get; set; }
            public string Status { get; set; }
            public IEnumerable<TransferItemsDto> TransferItems { get; set; }
            public class TransferItemsDto
            {
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public string Uom { get; set; }
                public decimal? Quantity { get; set; }
                public string ProductionDate { get; set; }
                public int? MoveOrderId { get; set; }
            }

        }

        public class Handler : IRequestHandler<GetTransferForReceivingQuery, PagedList<GetTransferResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetTransferResult>> Handle(GetTransferForReceivingQuery request, CancellationToken cancellationToken)
            {
                var adjustedDateTo = request.DateTo.AddDays(1);

                var transferOrders = _context.TransferOrders
                    .AsNoTracking()
                    .AsQueryable();


                if (!string.IsNullOrEmpty(request.TransferType))
                {
                    //Transfer Out
                    if (request.TransferType == Status.TransferOut)
                    {
                        transferOrders = transferOrders.Where(to => to.CreatedById == request.AccessBy);

                        if (!string.IsNullOrEmpty(request.Status))
                        {
                            if (request.Status == Status.ForReceiving)
                                transferOrders = transferOrders.Where(to => to.Status == Status.ForReceiving);
                            else if (request.Status == Status.Received)
                                transferOrders = transferOrders.Where(to => to.Status == Status.Received);
                            else if (request.Status == Status.Rejected)
                                transferOrders = transferOrders.Where(to => to.Status == Status.Rejected);
                        }
                    }
                    //Transfer In 
                    else if (request.TransferType == Status.TransferIn)
                    {
                        transferOrders = transferOrders.Where(to => to.TransferToId == request.AccessBy);

                        if (!string.IsNullOrEmpty(request.Status))
                        {
                            if (request.Status == Status.ForReceiving)
                                transferOrders = transferOrders.Where(to => to.Status == Status.ForReceiving);
                            else if (request.Status == Status.Received)
                                transferOrders = transferOrders.Where(to => to.Status == Status.Received);
                            else if (request.Status == Status.Rejected)
                                transferOrders = transferOrders.Where(to => to.Status == Status.Rejected);
                        }
                    }
                }

                transferOrders = transferOrders.Where(t => t.TransactionDate >= request.DateFrom && t.TransactionDate < adjustedDateTo);


                if (request.TransferOrderId != null)
                {
                    transferOrders = transferOrders.Where(to => to.Id == request.TransferOrderId);
                }



                if (!string.IsNullOrEmpty(request.Search))
                {
                    transferOrders = transferOrders.Where(to =>    
                        to.TransferTo.Fullname.Contains(request.Search));               
                }

                var result = transferOrders
                    .Select(to => new GetTransferResult
                    {
                        Id = to.Id,
                        CreatedBy = to.CreatedBy.Fullname,
                        CreatedByCluster = to.CreatedBy.CdoCluster.Cluster.ClusterType,
                        TransferTo = to.TransferTo.Fullname,
                        TransferToCluster = to.TransferTo.CdoCluster.Cluster.ClusterType,
                        TransactionType = to.TransactionType,
                        TotalQuantity = to.TotalQuantity,
                        TransactionDate = to.TransactionDate,
                        TransferType = to.TransferType,
                        Status = to.Status,
                        TransferItems = to.TransferOrderItems.Select(x => new GetTransferResult.TransferItemsDto
                        {
                            ItemCode = x.ItemCode,
                            ItemDescription = x.ItemDescription,
                            Uom = x.Uom,
                            Quantity = x.Quantity,
                            ProductionDate = x.ProductionDate,
                            MoveOrderId = x.MoveId
                        })
                    }).OrderByDescending(x => x.TransactionDate);

                return await PagedList<GetTransferResult>.CreateAsync(result, request.PageNumber, request.PageSize);

            }
        }
    }
}
