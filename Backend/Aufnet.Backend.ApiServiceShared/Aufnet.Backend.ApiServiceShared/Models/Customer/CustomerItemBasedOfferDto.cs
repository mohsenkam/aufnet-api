using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerItemBasedOfferDto
    {
        public string MerchantName { get; set; }
        public long CategryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public string ProductImage { get; set; }
    }
}