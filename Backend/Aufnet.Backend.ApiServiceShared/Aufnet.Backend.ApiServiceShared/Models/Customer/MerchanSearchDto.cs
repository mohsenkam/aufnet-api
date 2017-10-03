namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class MerchanSearchDto
    {
        public long MerchantId { get; set; }
        public string MerchantName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}