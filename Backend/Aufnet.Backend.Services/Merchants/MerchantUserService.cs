using System;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Shared.Exceptions;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchants;
using Aufnet.Backend.Data.Repository;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Merchants
{
    public class MerchantUserService : IMerchantUserService
    {
        private readonly IRepository<Merchant> _merRepository;
        private readonly Config _config;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantUserService( UserManager<ApplicationUser> userManager, IEmailService emailService, IRepository<Merchant> merRepository, Config config)
        {
            _userManager = userManager;
            _emailService = emailService;
            _merRepository = merRepository;
            _config = config;
        }


        public async Task<IServiceResult> SignUpAsync( MerchantSignUpDto value )
        {

            var serviceResult = new ServiceResult();

            try
            {
                var merchant = await _merRepository
                    .Query(m => m.Contract.Abn == value.Abn &&
                                m.Contract.BusinessName == value.BusinessName).FirstOrDefaultAsync();
                if (merchant == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                    return serviceResult;
                }

                if (_userManager.FindByEmailAsync(value.Email) != null) // This email is already assigned to another user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                    return serviceResult;
                }

                var user = new ApplicationUser
                {
                    UserName = value.Email,
                    Email = value.Email,
                };

                // Merchant is updated before actually adding the user
                merchant.User = user;
                await _merRepository.UpdateAsync(merchant);

                var result = await _userManager.CreateAsync(user, value.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        serviceResult.AddError(new ErrorMessage("", error.Description));
                    }

                    return serviceResult;
                }

                //User will be created but we don't assign him/her a role, until the email is confirmed

                var token = _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = _config.BaseUrl + "/auth/confirmemail?userId=" + user.Id + "&code=" + token;

                await _emailService.SendEmailAsync(new EmailModel
                {
                    ToEmail = value.Email,
                    Subject = "Complete your registration",
                    Body = $"Follow the link to activate your account: </br>{callbackUrl}"
                });
            }
            catch (Exception ex)
            {
                // todo: log the exception

                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
            }

            return serviceResult;

        }

        public async Task<IServiceResult> ConfirmEmailAsync( ConfirmEmailDto value )
        {

            var serviceResult = new ServiceResult();
            try
            {
                //validation
                if (String.IsNullOrEmpty(value.UserId))
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code,
                        ErrorCodesConstants.ArgumentMissing.Message + "CurrentPassword"));
                    return serviceResult;
                }
                if (String.IsNullOrEmpty(value.Code))
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code,
                        ErrorCodesConstants.ArgumentMissing.Message + "NewPassword"));
                    return serviceResult;
                }
                var user = await _userManager.FindByIdAsync(value.UserId);
                if (user == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                        ErrorCodesConstants.NotExistingUser.Message));
                    return serviceResult;
                }
                //end validation

                var result = await _userManager.ConfirmEmailAsync(user, value.Code);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        serviceResult.AddError(new ErrorMessage("", error.Description));
                    }
                }

                await AssignRole(user, RolesConstants.Merchant);
            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage("", ex.Message));
                return serviceResult;
            }
            return serviceResult;
        }

        public async Task<IServiceResult> ChangePasswordAsync( string username, MerchantChangePasswordDto value )
        {
            var serviceResult = new ServiceResult();

            //validation
            if (String.IsNullOrEmpty(value.CurrentPassword))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "CurrentPassword"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(value.NewPassword))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "NewPassword"));
                return serviceResult;
            }
            //end validation

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code, ErrorCodesConstants.NotExistingUser.Message));
                return serviceResult;
            }

            var result = await _userManager.ChangePasswordAsync(user, value.CurrentPassword, value.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    serviceResult.AddError(new ErrorMessage("", error.Description));
                }
            }
            return serviceResult;
        }


        public Task<IServiceResult> ResetPasswordByMail( string email )
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> ResetPasswordByPhone( string phone )
        {
            throw new NotImplementedException();
        }



        private async Task AssignRole( ApplicationUser user, string role )
        {
            if (user == null)
                throw new InvalidArgumentException("The argument user is null");
            if (String.IsNullOrEmpty(role))
                throw new InvalidArgumentException("The argument role is null or empty");

            var existingRoles = await _userManager.GetRolesAsync(user);
            if (!existingRoles.Contains(role))
                await _userManager.AddToRoleAsync(user, role);
        }
    }
}
