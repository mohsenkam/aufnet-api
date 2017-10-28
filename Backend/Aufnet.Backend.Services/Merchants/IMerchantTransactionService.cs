using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Merchants
{
    public interface IMerchantTransactionService
    {
        Task<IGetServiceResult<MerchantTransactionDetailsDto>> GetTransactionAsync(string username, long id);
        Task<IGetServiceResult<MerchantTransactionSummaryDto>> GetTransactionsAsync(string username);
    }
}