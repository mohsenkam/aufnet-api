using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Admin;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Admin
{
    public interface IAdmintransactionService
    {
        Task<IGetServiceResult<AdminTransactionDetailsDto>> GetTransactionAsync(long id);
        Task<IGetServiceResult<AdminTransactionSummaryDto>> GetTransactionsAsync();
    }

    public class AdminTransactionSummaryDto
    {
        public long TransactionId { get; set; }

        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        public long MerchantId { get; set; }
        public string MerchantName { get; set; }

    }
}