using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface ICustomerService
    {
        Task<IServiceResult> SignUpAsync(CustomerSignUpDto value); //Create user (customer)
        Task<IServiceResult> ChangePasswordAsync(string username, CustomerChangePasswordDto value); //Update user (customer)
        Task<IServiceResult> DeleteAsync( string username ); //Delete user (customer)
        Task<IServiceResult> ConfirmEmailAsync( ConfirmEmailDto value );
        Task<IServiceResult> ResetPasswordByMail(string email);
        Task<IServiceResult> ResetPasswordByPhone(string phone);
    }
}