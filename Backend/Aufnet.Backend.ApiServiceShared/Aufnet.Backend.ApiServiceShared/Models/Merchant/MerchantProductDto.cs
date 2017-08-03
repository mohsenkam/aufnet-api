
namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int Discount { get; set; }

    }
}