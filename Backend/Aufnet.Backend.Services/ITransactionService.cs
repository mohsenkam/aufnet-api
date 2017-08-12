using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.ApiServiceShared.Shared;
using System.Collections.Generic;

namespace Aufnet.Backend.Services
{
    public interface ITransactionService
    {
        Task<IGetServiceResult<List<TransactionDto>>> GetTransactionsAsync(string username);
        Task<IServiceResult> CreateTransaction(string username, TransactionDto value);
        Task<IGetServiceResult<TransactionDto>> GetTransactionDetailsAsync(string username, int value);
    }
}