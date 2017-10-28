using System;
using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantTransactionDetailsDto
    {
        public List<MerchantProductSummary> ProductSummaries { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }

    public class MerchantProductSummary
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Paid { get; set; } // Customer MAY pay less than the actual {{Price}} due to an offer
    }
}