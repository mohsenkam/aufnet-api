using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Transaction;

namespace Aufnet.Backend.Services
{
    public interface ITransactionService
    {
        Task<IGetServiceResult<TransactionDto>> GetCustomerTransactionsAsync(string username, TransactionDto value);
        Task<IGetServiceResult<TransactionDto>> GetMerchantTransactionsAsync(string username, TransactionDto value);
        Task<IServiceResult> CreateTransaction(string username, TransactionDto value);
    }
}