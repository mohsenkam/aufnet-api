using System;
using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerTransactionDetailsDto
    {
        public List<ProductSummary> ProductSummaries { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }

    public class ProductSummary
    {
        public string ProductName { get; set; }
        public string ProductFullCategory { get; set; }
        public decimal Price { get; set; }
        public decimal Paid { get; set; } // You MAY pay less than the actual {{Price}} due to an offer
        public string MerchantName { get; set; }
    }
}