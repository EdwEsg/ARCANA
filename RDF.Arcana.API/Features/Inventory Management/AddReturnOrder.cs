using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class AddReturnOrder
    {
        public class AddReturnOrderCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public int AccessBy { get; set; }
            public ICollection<ReturnOrdersDto> ReturnOrders { get; set; }
            public ICollection<ReplaceOrdersDto> ReplaceOrders { get; set; }
            public class ReturnOrdersDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public string Bbd { get; set; }
            }

            public class ReplaceOrdersDto
            {
                public int ItemId { get; set; }
                public decimal Quantity { get; set; }
                public decimal Price { get; set; }
                public string Bbd { get; set; }
            }
        }

        //public class Handler : IRequestHandler<AddReturnOrderCommand, Result>
        //{
        //    private readonly ArcanaDbContext _context;
        //    public Handler(ArcanaDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<Result> Handle(AddReturnOrderCommand request, CancellationToken cancellationToken)
        //    {
        //        bool isUserCdo = await _context.Users
        //            .Where(u => u.Id == request.AccessBy)
        //            .Select(u => u.UserRolesId)
        //            .FirstOrDefaultAsync(cancellationToken) == 6; //Cdo

        //        if (!isUserCdo)
        //        {
        //            return InventoryErrors.NotUserCdo();
        //        }

        //        bool isClientExist = _context.Clients
        //            .Any(c => c.Id == request.ClientId);

        //        if (!isClientExist)
        //        {
        //            return InventoryErrors.NoClientFound();
        //        }

        //        var replaceOrders = request.ReplaceOrders
        //            .GroupBy(o => o.ItemId)
        //            .Select(g => new
        //            {
        //                ItemId = g.Key,
        //                RequestedQuantity = g.Sum(o => o.Quantity),
        //            }).ToList();

        //        var cdoMoveOrderItems = await _context.MoveOrderItems
        //            .Where(m => m.CreatedBy.Id == request.AccessBy)
        //            .GroupBy(i => i.ItemId)
        //            .Select(g => new
        //            {
        //                ItemId = g.Key,
        //                AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
        //            }).ToListAsync(cancellationToken);

        //        var cdoTransferOrderItems = await _context.TransferOrderItems
        //            .Where(t => t.TransferOrder.TransferToId == request.AccessBy && 
        //                t.TransferOrder.Status == Status.Received)
        //            .GroupBy (i => i.ItemId)
        //            .Select(g => new
        //            {
        //                ItemId = g.Key,
        //                AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
        //            }).ToListAsync(cancellationToken);

        //        var totalOrderItems = cdoMoveOrderItems
        //            .Concat(cdoTransferOrderItems)
        //            .GroupBy (x => x.ItemId)
        //            .Select(g => new
        //            {
        //                ItemId = g.Key,
        //                AvailableQuantity = g.Sum(x => x.AvailableQuantity)
        //            }).ToList();

        //        foreach (var item in replaceOrders)
        //        {
        //            var itemCode = await _context.Items
        //                .Where(i => i.Id == item.ItemId)
        //                .Select(i => i.ItemCode)
        //                .FirstOrDefaultAsync(cancellationToken);

        //            var availableItems = totalOrderItems.FirstOrDefault(a => a.ItemId == item.ItemId);

        //            if (availableItems == null)
        //            {
        //                return InventoryErrors.ItemNotFound(itemCode);
        //            }

        //            if (item.RequestedQuantity > availableItems.AvailableQuantity)
        //            {
        //                return InventoryErrors.InsufficientQuantity(
        //                    itemCode,
        //                    item.RequestedQuantity,
        //                    availableItems.AvailableQuantity
        //                );
        //            }
        //        }

        //        foreach (var item in replaceOrders)
        //        {
        //            decimal quantityToDeduct = item.RequestedQuantity;
        //        }

        //    }
        //}
    }
}
