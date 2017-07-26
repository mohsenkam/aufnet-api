using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public interface IMerchantProfileService
    {
        Task<IServiceResult> CreateProfile(MerchantProfileDto merchantProfileDto);
        Task<IServiceResult> UpdateProfile(string username, MerchantProfileDto newMerchantProfileDto);
        Task<IServiceResult> DelteProfile(string username);
    }
}