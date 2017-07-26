using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Services.Base;

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