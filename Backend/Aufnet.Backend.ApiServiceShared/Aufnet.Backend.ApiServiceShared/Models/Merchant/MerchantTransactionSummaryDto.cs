using System;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantTransactionSummaryDto
    {
        public decimal TotalValue { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}