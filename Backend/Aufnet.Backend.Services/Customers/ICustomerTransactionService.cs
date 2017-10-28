using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Customers
{
    public interface ICustomerTransactionService
    {
        Task<IServiceResult> SaveTransactionAsync(SaveTransactionDto value);
        Task<IGetServiceResult<CustomerTransactionDetailsDto>> GetTransactionAsync(string username, long id);
        Task<IGetServiceResult<CustomerTransactionSummaryDto>> GetTransactionsAsync(string username);

    }
}