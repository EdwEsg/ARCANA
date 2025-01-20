
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class GetAllItemsInInventory
    {
        public class GetAllItemsInInventoryQuery : IRequest<GetAllItemsInInventoryResult> 
        {
            public int AccessBy { get; set; }
        }

        public class GetAllItemsInInventoryResult
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal AvailQuantity { get; set; }
        }

        //public class Handler : IRequestHandler<GetAllItemsInInventoryQuery, GetAllItemsInInventoryResult>
        //{
        //    private readonly ArcanaDbContext _context;
        //    public Handler(ArcanaDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public Task<GetAllItemsInInventoryResult> Handle(GetAllItemsInInventoryQuery request, CancellationToken cancellationToken)
        //    {
        //        var moveOrderItems = _context.MoveOrderItems
        //            .Where(rq => rq.RemainingQuantity > 0 && rq.MoveOrder.CreatedById == request.AccessBy)
        //            .GroupBy(x => x.ItemCode)
        //            .Select(i => new
        //            {
        //                ItemCode = i.Key,
        //                AvailableQuantity = i.Sum(x => x.RemainingQuantity)
        //            });

        //        var transferInItems = _context.TransferOrderItems
        //            .Where(rq => rq.RemainingQuantity > 0 && rq.TransferOrder.CreatedById == request.AccessBy)
        //            .GroupBy(x => x.ItemCode)
        //            .Select(i => new
        //            {
        //                ItemCode = i.Key,
        //                AvailableQuantity = i.Sum(x => x.RemainingQuantity)
        //            });

        //        var returnOderItems = _context.ReturnOrderItems
        //            .Where(rq => rq.Quantity > 0 && rq.TransferOrder.CreatedById == request.AccessBy)
        //            .GroupBy(x => x.ItemCode)
        //            .Select(i => new
        //            {
        //                ItemCode = i.Key,
        //                AvailableQuantity = i.Sum(x => x.RemainingQuantity)
        //            });
        //    }
        //}
    }
}
