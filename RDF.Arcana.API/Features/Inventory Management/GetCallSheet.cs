using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Pagination;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    public class GetCallSheet
    {
        public class GetCallSheetQuery : UserParams, IRequest<PagedList<GetCallSheetResult>>
        {
            public string Search { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? TransactionId { get; set; }
            public int? TransactionItemId { get; set; }
            public int AccessBy { get; set; }
        }

        public class GetCallSheetResult
        {
            public int TransactionId { get; set; }
            public string CustomerName { get; set; }
            public string BusinessName { get; set; }
            public DateTime CallSheetDate { get; set; }
            public int Red { get; set; }
            public int Orange { get; set; }
            public int Green { get; set; }
            public IEnumerable<TransactionItemsDto> TransactionItemsDtos { get; set; }
            public class TransactionItemsDto
            {
                public int TransactionItemId { get; set; }
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public decimal SalesIn { get; set; }
                public decimal RemainingInv { get; set; }
                public decimal EndingInv { get; set; }
                public decimal SalesOut { get; set; }
                public decimal SuggestedPo { get; set; }
                public decimal AverageSales { get; set; }
                public IEnumerable<BbbDto> bbbDtos { get; set; }
                public class BbbDto
                {
                    public decimal Quantity { get; set; }
                    public DateTime Bbd { get; set; }
                }
            }
        }
    }
}
