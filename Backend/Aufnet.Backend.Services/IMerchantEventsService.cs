using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface IMerchantEventsService
    {
        Task<IGetServiceResult<MerchantEventsDto>> GetEvents(string username);
        Task<IServiceResult> AddEvent(string username, MerchantEventsDto value);
        Task<IServiceResult> UpdateEvent(string username, MerchantEventsDto value);
        Task<IServiceResult> RemoveEvent(string username, int merchantEventId);
    }
}