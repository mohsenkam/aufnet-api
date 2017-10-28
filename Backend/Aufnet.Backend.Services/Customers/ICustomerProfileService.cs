using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Customers
{
    public interface ICustomerProfileService
    {
        Task<IGetServiceResult<CustomerProfileDto>> GetProfileAsync(string username);
        Task<IServiceResult> CreateProfileAsync(string username, CustomerProfileDto value);
        Task<IServiceResult> UpdateProfileAsync(string username, CustomerProfileDto value);
        Task<IServiceResult> DeleteProfileAsync(string username);
    }
}