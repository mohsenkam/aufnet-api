using System.Threading.Tasks;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public interface ICustomerService
    {
        Task<IServiceResult> SignUpAsync(string username, string password, string role);
        Task<IServiceResult> ChangePasswordAsync(string username, string currentPassword, string newPassword);
        IServiceResult SignIn(string username, string password);
        IServiceResult ResetPasswordByMail(string email);
        IServiceResult ResetPasswordByPhone(string phone);
    }
}