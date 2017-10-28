namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class ProductSearchDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string MerchantName { get; set; }
        public long MerchantId { get; set; }
        public decimal NewPrice { get; set; }
    }
}