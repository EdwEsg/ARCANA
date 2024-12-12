using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class GetTransfer
    {
        public class GetTransferForReceivingQuery : UserParams, IRequest<PagedList<GetTransferResult>>
        {
            public int? TransferOrderId { get; set; }
            public string TransferType { get; set; }
            public string Status { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetTransferResult
        {
            public string To { get; set; }
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
                public decimal Quantity { get; set; }
                public string ProductionDate { get; set; }
                public string MoveOrderId { get; set; }
            }

        }

        //public class Handler : IRequestHandler<GetTransferForReceivingQuery, PagedList<GetTransferResult>>
        //{
        //    private readonly ArcanaDbContext _context;
        //    public Handler(ArcanaDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<PagedList<GetTransferResult>> Handle(GetTransferForReceivingQuery request, CancellationToken cancellationToken)
        //    {
        //        var transferOrders = _context.TransferOrders
        //            .AsNoTracking()
        //            .Include(toi => toi.TransferOrderItems)
        //            .AsQueryable();

        //        if (request.AccessBy != 1)
        //        {
        //            transferOrders = transferOrders.Where(to => to.CreatedById == request.AccessBy);
        //        }

        //        if (request.TransferOrderId != null) 
        //        {
        //            transferOrders = transferOrders.Where(to => to.Id == request.TransferOrderId);
        //        }

                

        //    }
        //}
    }
}
