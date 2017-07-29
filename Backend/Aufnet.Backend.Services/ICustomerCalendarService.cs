using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Customer;

namespace Aufnet.Backend.Services
{
    public interface ICustomerCalendarService
    {
        Task<IGetServiceResult<CustomerCalendarDto>> GetEventsAsync(string username);
        Task<IServiceResult> AddEventBookmarkAsync(string username, int merchantEventId);
        Task<IServiceResult> RemoveEventBookmarkAsync(string username, int merchantEventId);
    }
}