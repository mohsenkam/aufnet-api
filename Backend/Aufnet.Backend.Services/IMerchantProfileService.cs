using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public interface IMerchantProfileService
    {
        Task<IGetServiceResult<MerchantProfileDto>> GetProfileAsync(string username);
        Task<IServiceResult> CreateProfile(string username, MerchantProfileDto value);
        Task<IServiceResult> UpdateProfile(string username, MerchantProfileDto value);
        Task<IServiceResult> DeleteProfile(string username);
    }
}