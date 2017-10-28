using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerMerchantDto
    {
        public string MerchantName { get; set; }
        public long CategryId { get; set; }
        public string CategoryName { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public List<ItemBasedOfferThumbnailDto> ItemBasedOffers { get; set; }
        public List<QuantityBasedOfferThumbnailDto> QuantityBasedOffers { get; set; }
    }
}