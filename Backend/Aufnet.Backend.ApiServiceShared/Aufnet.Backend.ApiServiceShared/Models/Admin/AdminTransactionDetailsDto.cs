using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Admin
{
    public class AdminTransactionDetailsDto
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        public long MerchantId { get; set; }
        public string MerchantName { get; set; }

        public List<AdminProductSummary> AdminProductSummaries { get; set; }
    }

    public class AdminProductSummary
    {
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Paid { get; set; }
        public long OfferId { get; set; }
        public string OfferType { get; set; }
    }
}