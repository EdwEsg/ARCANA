using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Models.ExternalDb;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class AddMoveOrderReceiving
    {
        public class AddMoveOrderReceivingCommand : IRequest<Result>
        {
            public int MoveOrderId { get; set; }
        }

        //public class Handler : IRequestHandler<AddMoveOrderReceivingCommand, Result>
        //{
        //    private readonly ArcanaDbContext _context;
        //    private readonly ExternalDbContext _externalDbContext;
        //    public Handler(ArcanaDbContext context, ExternalDbContext externalDbContext)
        //    {
        //        _context = context;
        //        _externalDbContext = externalDbContext;
        //    }

        //    public async Task<Result> Handle(AddMoveOrderReceivingCommand request, CancellationToken cancellationToken)
        //    {
        //        var moveOrder = await _externalDbContext.MoveOrders.Where(mo => mo.Id == request.MoveOrderId)
        //            .Select(mo => new { mo.Id, mo.Description, mo.MoveOrderTransactDate, mo.MeatType })
        //            .FirstOrDefaultAsync();
                    
        //        if (moveOrder == null)
        //        {
        //            return InventoryErrors.NotFound();
        //        }


        //    }
        //}
    }
}
