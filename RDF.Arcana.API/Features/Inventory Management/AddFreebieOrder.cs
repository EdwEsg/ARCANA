using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class AddFreebieOrder
    {
        public class AddFreebieOrderCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public int AccessBy { get; set; }
            public ICollection<FreebieOrderItemDto> FreebieOrderItems { get; set; }
            public class FreebieOrderItemDto
            {
                public int ItemId { get; set; }
                public decimal? Quantity { get; set; }
            }
        }


        //public class Handler : IRequestHandler<AddFreebieOrderCommand, Result>
        //{
        //    private readonly ArcanaDbContext _context;
        //    public Handler(ArcanaDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<Result> Handle(AddFreebieOrderCommand request, CancellationToken cancellationToken)
        //    {
        //        var isUserCdo = await _context.Users
        //            .Where(u => u.Id == request.AccessBy)
        //            .Select(u => u.UserRolesId)
        //            .FirstOrDefaultAsync(cancellationToken) == 6;

        //        if (!isUserCdo)
        //        {
        //            return InventoryErrors.NotUserCdo();
        //        }

        //        bool existingClient = _context.Clients
        //            .Any(u => u.Id == request.ClientId);

        //        if (!existingClient)
        //        {
        //            return InventoryErrors.NoClientFound();
        //        }

        //        var requestedItems = request.FreebieOrderItems
        //            .GroupBy(i => i.ItemId)
        //            .Select(g => new
        //            {
        //                ItemId = g.Key,
        //                RequestedQuantity = g.Sum(i => i.Quantity ?? 0)
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
        //            .Where(t => t.TransferOrder.TransferToId == request.AccessBy)
        //            .GroupBy(i => i.ItemId)
        //            .Select(g => new
        //            {
        //                ItemId = g.Key,
        //                AvailableQuantity = g.Sum(i => i.RemainingQuantity ?? 0)
        //            }).ToListAsync(cancellationToken);

        //        var totalOrderItems = (cdoMoveOrderItems.AvailableQuantity + cdoTransferOrderItems.AvailableQuantity).where(ItemId are the same)

        //    }
        //}
    }
}
