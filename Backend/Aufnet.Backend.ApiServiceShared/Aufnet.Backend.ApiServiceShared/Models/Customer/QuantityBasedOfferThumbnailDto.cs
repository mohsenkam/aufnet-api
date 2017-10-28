using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class QuantityBasedOfferThumbnailDto
    {
        public int ProductsCount { get; set; }
        public int Discount { get; set; }
        public decimal MinTotalValue { get; set; }
        public long OfferId { get; set; }
    }
}