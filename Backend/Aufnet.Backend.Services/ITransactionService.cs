using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.ApiServiceShared.Shared;
using System.Collections.Generic;

namespace Aufnet.Backend.Services
{
    public interface ITransactionService
    {
        Task<IGetServiceResult<List<TransactionDto>>> GetCustomerTransactionsAsync(string username);
        Task<IGetServiceResult<List<TransactionDto>>> GetMerchantTransactionsAsync(string username);
        Task<IServiceResult> CreateTransaction(string username, TransactionDto value);
        Task<IGetServiceResult<TransactionDto>> GetCustomerTransactionAsync(string username, int value);
        Task<IGetServiceResult<TransactionDto>> GetMerchantTransactionAsync(string username, int value);
    }
}