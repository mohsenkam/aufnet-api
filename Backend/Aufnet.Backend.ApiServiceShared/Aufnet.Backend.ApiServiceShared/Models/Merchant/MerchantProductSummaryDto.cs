using System;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantProductSummaryDto
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public Decimal Price { get; set; }
    }
}