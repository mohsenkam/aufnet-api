namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerProductDto
    {
        public string Name { get; set; }
        public decimal OldPrice { get; set; }
        public decimal? NewPrice { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public string Descripion { get; set; }
        public string MerchantName { get; set; }
        public long MerchantId { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
    }
}