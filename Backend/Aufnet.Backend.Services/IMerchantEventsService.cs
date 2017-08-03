using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface IMerchantEventsService
    {
        Task<IGetServiceResult<MerchantEventsDto>> GetEventAsync(string username);
        Task<IServiceResult> CreateEvent(string username, MerchantEventsDto value);
        Task<IServiceResult> UpdateEvent(string username, MerchantEventsDto value);
        Task<IServiceResult> DeleteEvent(string username, int merchantEventId);
    }
}