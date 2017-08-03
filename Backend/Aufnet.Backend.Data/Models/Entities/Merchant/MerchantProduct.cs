
namespace Aufnet.Backend.Data.Models.Entities.Merchant
{
    public class MerchantProduct :Entity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int Discount { get; set; }
    }
}
