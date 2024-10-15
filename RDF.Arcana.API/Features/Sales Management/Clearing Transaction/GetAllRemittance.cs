//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Common.Pagination;
//using RDF.Arcana.API.Data;

//namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
//{
//    public class GetAllRemittance
//    {
//        public class GetAllRemittanceQuery : UserParams, IRequest<PagedList<Unit>>
//        {
//            public string Search { get; set; }
//            public string Status { get; set; }
//            public int? ClusterId { get; set; }
//            public int AddedBy { get; set; }
//            public string PaymentMethod { get; set; }
//        }

//        public class GetAllRemittanceResult
//        {
//            public string BusinessName { get; set; }
//            public string OwnersName { get; set; }
//            public string PaymentMethod { get; set; }
//            public string PaymentChannel { get; set; }
//            public string ReferenceNo { get; set; }
//            public decimal TotalPaymentAmount { get; set; }
//            public string Reason { get; set; }
//            public string ATag { get; set; }
//            public DateTime Date { get; set; }
//            public ICollection<Invoice> Invoices { get; set; }
//            public class Invoice
//            {
//                public string InvoiceNo { get; set; }
//            }
//        }

//        public class Handler : IRequestHandler<GetAllRemittanceQuery, PagedList<GetAllRemittanceResult>>
//        {
//            private readonly ArcanaDbContext _context;
//            public Handler(ArcanaDbContext context)
//            {
//                _context = context;
//            }

//            public Task<PagedList<GetAllRemittanceResult>> Handle(GetAllRemittanceQuery request, CancellationToken cancellationToken)
//            {
//                var paymentTransactions = _context.PaymentTransactions
//                    .Where(pt => pt.PaymentRecord.Status == request.Status &&
//                             pt.Transaction.Status != Status.Cancelled &&
//                             pt.Transaction.Status != Status.Voided &&
//                             pt.Status != Status.Voided &&
//                             pt.Status != Status.Cancelled &&
//                             pt.TotalAmountReceived > 0);
//            }
//        }
//    }
//}
