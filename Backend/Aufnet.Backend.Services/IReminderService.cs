using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Reminder;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface IReminderService
    {
        Task<IServiceResult> CreateReminder(string username, ReminderDto value);
        //
        Task<IServiceResult> UpdateReminder(string username, ReminderDto value);
        //
        Task<IServiceResult> DeleteReminder(string username, int reminderId);
    }
}