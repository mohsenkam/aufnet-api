using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Customers
{
    public interface ICustomerUserService
    {
        /// <summary>
        /// Creates the Merchant and a User, which is assigned to the merchant.
        /// This method can create a Merchant with the same email again if creating the User fails.
        /// </summary>
        Task<IServiceResult> SignUpAsync(CustomerSignUpDto value); //Create user (customer)
        Task<IServiceResult> ChangePasswordAsync(string username, CustomerChangePasswordDto value); //Update user (customer)
        Task<IServiceResult> DeleteAsync( string username ); //Delete user (customer)
        Task<IServiceResult> ConfirmEmailAsync( ConfirmEmailDto value );
        Task<IServiceResult> ResetPasswordByMail(string email);
        Task<IServiceResult> ResetPasswordByPhone(string phone);
    }
}