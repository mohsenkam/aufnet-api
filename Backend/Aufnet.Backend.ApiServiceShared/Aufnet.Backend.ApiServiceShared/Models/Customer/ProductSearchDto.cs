namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class ProductSearchDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}