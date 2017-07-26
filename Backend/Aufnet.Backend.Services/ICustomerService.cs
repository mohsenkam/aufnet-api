using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public interface ICustomerService
    {
        Task<IServiceResult> SignUpAsync(CustomerSignUpDto value);
        Task<IServiceResult> ChangePasswordAsync(CustomerChangePasswordDto value);
        IServiceResult SignIn(string username, string password);
        IServiceResult ResetPasswordByMail(string email);
        IServiceResult ResetPasswordByPhone(string phone);
    }
}