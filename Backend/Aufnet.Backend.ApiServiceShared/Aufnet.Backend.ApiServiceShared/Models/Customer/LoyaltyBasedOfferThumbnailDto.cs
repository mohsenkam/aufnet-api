namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class LoyaltyBasedOfferThumbnailDto
    {
        public decimal MinTotalValue { get; set; }
        public int ProductsCount { get; set; }
        public int Discount { get; set; }
        public long OfferId { get; set; }
        public int TransactionTimes { get; set; }
    }
}