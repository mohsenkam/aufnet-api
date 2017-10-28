namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class ItemBasedOfferThumbnailDto
    {
        public long OfferId { get; set; }
        public string MerchantName { get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public string ProductImage { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}