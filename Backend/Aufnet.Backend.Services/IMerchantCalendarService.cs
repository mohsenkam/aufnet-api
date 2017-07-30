using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;


namespace Aufnet.Backend.Services
{
    public interface IMerchantCalendarService
    {
        Task<IGetServiceResult<MerchantCalendarDto>> GetEventsAsync(string username);
        Task<IServiceResult> AddEventBookmarkAsync(string username, int merchantEventId);
        Task<IServiceResult> RemoveEventBookmarkAsync(string username, int merchantEventId);
    }
}