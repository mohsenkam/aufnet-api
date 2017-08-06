using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public interface IMerchantService
    {
        Task<IGetServiceResult<MerchantSignUpDto>> GetMerchantAsync(string username);
        Task<IGetServiceResult<MerchantSignUpDto>> GetMerchantAsync();
        Task<IServiceResult> SignUpAsync(MerchantSignUpDto value); //Create user (customer)
        Task<IServiceResult> ChangePasswordAsync(string username, MerchantChangePasswordDto value); //Update user (customer)
        Task<IServiceResult> DeleteAsync(string username); //Delete user (customer)
        Task<IServiceResult> ConfirmEmailAsync(ConfirmEmailDto value);
        Task<IServiceResult> ResetPasswordByMail(string email);
        Task<IServiceResult> ResetPasswordByPhone(string phone);
        Task<IGetServiceResult<List<MerchantProfileDto>>> SearchMerchantByAddress(AddressDto addressDto);
    }
}