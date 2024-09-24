using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class Cheque : BaseEntity
    {
        public int ClientId { get; set; }
        public int? ListingFeeId { get; set; }
        public int? PaymentTransactionId { get; set; }
        public string Bank { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string VoucherNo { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingBalance { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual PaymentTransaction PaymentTransaction { get; set; }
        public virtual Clients Client { get; set; }
        public virtual ListingFee ListingFee { get; set; }
    }
}
