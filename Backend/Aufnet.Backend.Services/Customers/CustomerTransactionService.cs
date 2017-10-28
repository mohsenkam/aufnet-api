using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Customers
{
    class CustomerTransactionService : ICustomerTransactionService
    {
        public Task<IServiceResult> SaveTransactionAsync(SaveTransactionDto value)
        {
            throw new System.NotImplementedException();
        }

        public Task<IGetServiceResult<CustomerTransactionDetailsDto>> GetTransactionAsync(string username, long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IGetServiceResult<CustomerTransactionSummaryDto>> GetTransactionsAsync(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}