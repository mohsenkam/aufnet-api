using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Reminder;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface IReminderService
    {
        Task<IServiceResult> CreateMerchantReminder(string username, ReminderDto value);
        Task<IServiceResult> CreateCustomerReminder(string username, ReminderDto value);
        //
        Task<IServiceResult> UpdateMerchantReminder(string username, ReminderDto value);
        Task<IServiceResult> UpdateCustomerReminder(string username, ReminderDto value);
        //
        Task<IServiceResult> DeleteMerchantReminder(string username, int merchantReminderId);
        Task<IServiceResult> DeleteCustomerReminder(string username, int customerReminderId);
    }
}