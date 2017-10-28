using System;
using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerLoyaltyBasedOfferDto
    {
        public string MerchantName { get; set; }
        public long MerchantId { get; set; }
        public decimal MinTotalValue { get; set; }
        public List<ProductSearchDto> Products { get; set; }
        public int Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TransactionTimes { get; set; }
    }
}