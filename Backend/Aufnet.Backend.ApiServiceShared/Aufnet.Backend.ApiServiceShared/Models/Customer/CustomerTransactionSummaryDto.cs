using System;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerTransactionSummaryDto
    {
        public DateTime TransactionDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPaid { get; set; }
    }
}