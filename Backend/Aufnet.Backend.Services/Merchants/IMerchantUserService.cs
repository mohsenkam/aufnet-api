using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Microsoft.AspNetCore.Http;

namespace Aufnet.Backend.Services.Merchants
{
    public interface IMerchantUserService
    {
        /// <summary>
        /// Create the user corresponding the Merchant entity represented by the parameter Abn and BusinessName.
        /// Then, sends a confirmation email to the merchant.
        /// This method can overwrite the user of the merchant.
        /// </summary>
        /// <param name="value">Abn and BusinessName represent the Merchant entity. Other properties represent the details
        /// to be used to create the user.</param>
        Task<IServiceResult> SignUpAsync( MerchantSignUpDto value );
        Task<IServiceResult> ChangePasswordAsync( string username, MerchantChangePasswordDto value );
        Task<IServiceResult> ConfirmEmailAsync( ConfirmEmailDto value );
        Task<IServiceResult> ResetPasswordByMail( string email );
        Task<IServiceResult> ResetPasswordByPhone( string phone );
    }
}