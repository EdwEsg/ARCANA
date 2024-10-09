using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class Cheque : BaseEntity
    {
        public int ClientId { get; set; }
        //this is where the amountToConvert started to deduct in collections of listing fees
        public int? ListingFeeId { get; set; }
        public string Bank { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string VoucherNo { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingBalanceListing { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual User AddedByUser { get; set; }
        public virtual Clients Client { get; set; }
        public virtual ListingFee ListingFee { get; set; }
    }
}
