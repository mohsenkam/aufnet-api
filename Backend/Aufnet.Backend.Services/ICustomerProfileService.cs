using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface ICustomerProfileService
    {
        Task<IGetServiceResult<CustomerProfileDto>> GetProfileAsync(string username);
        Task<IServiceResult> CreateProfile();
        Task<IServiceResult> UpdateProfile();
        Task<IServiceResult> DelteProfile();
    }
}