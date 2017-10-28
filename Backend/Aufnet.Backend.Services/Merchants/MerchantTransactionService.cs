using System;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Merchants
{
    class MerchantTransactionService : IMerchantTransactionService
    {
        public Task<IGetServiceResult<MerchantTransactionDetailsDto>> GetTransactionAsync(string username, long id)
        {
            throw new NotImplementedException();
        }

        public Task<IGetServiceResult<MerchantTransactionSummaryDto>> GetTransactionsAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}