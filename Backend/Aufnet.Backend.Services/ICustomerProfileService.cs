using System.Threading.Tasks;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public interface ICustomerProfileService
    {
        Task<IServiceResult> CreateProfile();
        Task<IServiceResult> UpdateProfile();
        Task<IServiceResult> DelteProfile();
    }
}