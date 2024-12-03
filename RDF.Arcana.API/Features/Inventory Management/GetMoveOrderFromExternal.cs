
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Models.ExternalDb;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/get-mo-from-external"), ApiController]
    public class GetMoveOrderFromExternal : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetMoveOrderFromExternal(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var query = new GetMoveOrderFromExternalQuery
                {
                    MoveOrderId = id
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public class GetMoveOrderFromExternalQuery : IRequest<Result>
        {
            public int MoveOrderId { get; set; }
        }

        public class GetMoveOrderFromExternalResult
        {
            public string CustomerName { get; set; }
            public string Route { get; set; }
            public string Details { get; set; }
            public string Area { get; set; }
            public DateTime TransactionDate { get; set; }
            public int MoveOrderId { get; set; }
            public DateTime DeliveryDate { get; set; }

            public ICollection<MoItem> MoItems { get; set; }
            public class MoItem
            {
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public string Uom { get; set; }
                public decimal Quantity { get; set; }
                public string ProductionDate { get; set; }
            }
            
        }

        public class Handler : IRequestHandler<GetMoveOrderFromExternalQuery, Result>
        {
            private readonly ExternalDbContext _external;
            public Handler(ExternalDbContext external)
            {
                _external = external;
            }

            public async Task<Result> Handle(GetMoveOrderFromExternalQuery request, CancellationToken cancellationToken)
            {
                var moveOrder = await (from mo in _external.MoveOrders
                                       where mo.Id == request.MoveOrderId
                                       join cust in _external.Customers on mo.CustomerId equals cust.Id into custGroup
                                       from cust in custGroup.DefaultIfEmpty()
                                       join area in _external.Areas on cust.AreaId equals area.Id into areaGroup
                                       from area in areaGroup.DefaultIfEmpty()
                                       select new
                                       {
                                           mo,
                                           CustomerName = cust != null ? cust.CustomerName : string.Empty,
                                           Route = cust != null ? cust.Address : string.Empty,
                                           Details = mo.Description,
                                           Area = area != null ? area.Area1 : string.Empty,
                                           TransactionDate = mo.TransactionDate ?? DateTime.MinValue,
                                           MoveOrderId = mo.Id,
                                           DeliveryDate = mo.DeliveryDate ?? DateTime.MinValue
                                       }).FirstOrDefaultAsync(cancellationToken);

                if (moveOrder == null)
                {
                    return InventoryErrors.MoNotFound();
                }

                var moItems = await (from moi in _external.MoveOrderItems
                                     where moi.MoveId == request.MoveOrderId
                                     join rm in _external.RmMasterlists on moi.ItemId equals rm.Int
                                     join uom in _external.Uoms on rm.UomId equals uom.Id into uomGroup
                                     from uom in uomGroup.DefaultIfEmpty()
                                     select new GetMoveOrderFromExternalResult.MoItem
                                     {
                                         ItemCode = rm.ItemCode,
                                         ItemDescription = rm.ItemDescription,
                                         Uom = uom != null ? uom.UomDescription : string.Empty,
                                         Quantity = (decimal)(moi.Quantity ?? 0),
                                         ProductionDate = moi.ProductionDate
                                     }).ToListAsync(cancellationToken);

                var result = new GetMoveOrderFromExternalResult
                {
                    CustomerName = moveOrder.CustomerName,
                    Route = moveOrder.Route,
                    Details = moveOrder.Details,
                    Area = moveOrder.Area,
                    TransactionDate = moveOrder.TransactionDate,
                    MoveOrderId = moveOrder.MoveOrderId,
                    DeliveryDate = moveOrder.DeliveryDate,
                    MoItems = moItems
                };

                return Result.Success(result);
            }
        }
    }
}
