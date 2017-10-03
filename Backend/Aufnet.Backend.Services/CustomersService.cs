using System;
using System.Linq;
using System.Threading.Tasks;
//using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Shared.Exceptions;
using Aufnet.Backend.Data.Context;

using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;

using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Aufnet.Backend.Services
{
    public class CustomersService: ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomersService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _emailService = emailService;
        }

        
        public async Task<IServiceResult> SignUpAsync(CustomerSignUpDto value)
        {
            
            var serviceResult = new ServiceResult();


            // check if the use already exists!

            ApplicationUser existingUser = await _userManager.FindByEmailAsync(value.Email);

            if (existingUser != null)
            { 
                // the user already exists
                if (await _userManager.IsEmailConfirmedAsync(existingUser)) 
                { 
                    // the user has confirmed his/her email
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.AddingDuplicateEntry.Code,
                        ErrorCodesConstants.AddingDuplicateEntry.Message));
                    return serviceResult;
                }
            }
            ApplicationUser user;
            if (existingUser == null)
            {
                // the user doesn't exist
                user = new ApplicationUser
                {
                    UserName = value.Email,
                    Email = value.Email,
                };

                var result = await _userManager.CreateAsync(user, value.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        serviceResult.AddError(new ErrorMessage("", error.Description));
                    }
                    return serviceResult;
                }
            }
            else
            {
                // the user exists but has not confirmed his/her email
                user = existingUser;
            }

            //User will be created but we don't assign him/her a role, until the email is confirmed

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = "http://localhost:4200/auth/confirmemail?userId=" + user.Id + "&code="+token;
            //Url.Action(controller: "Auth", action: "ResetPassword",
            //values: new { userId = userId, code = code }, protocol: "https", host: "aufnet.com.au");
            await _emailService.SendEmailAsync(new EmailModel
            {
                ToEmail = value.Email,
                Subject = "Confirm your email",
                Body = $"Follow the link to confirm your email: </br>{callbackUrl}"
            });

            return serviceResult;

        }

        public async Task<IServiceResult> ConfirmEmailAsync(ConfirmEmailDto value)
        {

            var serviceResult = new ServiceResult();

            //validation
            if (String.IsNullOrEmpty(value.UserId))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "CurrentPassword"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(value.Code))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "NewPassword"));
                return serviceResult;
            }
            var user = await _userManager.FindByIdAsync(value.UserId);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code, ErrorCodesConstants.InvalidOperation.Message));
                return serviceResult;
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.RepeatedOperation.Code, ErrorCodesConstants.RepeatedOperation.Message));
                return serviceResult;
            }

            //end validation
            
            var result = await _userManager.ConfirmEmailAsync(user, value.Code);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    // todo: log the error.Description as this is an internal error!
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code, ErrorCodesConstants.OperationFailed.Message));
                }
            }
            try
            {
                await AssignRole(user, RolesConstants.Customer);
            }
            catch (InvalidArgumentException exception)
            {
                //todo: log the exception.Message as this is an internal error!
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code, ErrorCodesConstants.OperationFailed.Code));
                return serviceResult;
            }
            return serviceResult;
        }

        public async Task<IServiceResult> ChangePasswordAsync( string username, CustomerChangePasswordDto value )
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
            //_context.SaveChanges();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    serviceResult.AddError(new ErrorMessage("", error.Description));
                }
            }
            return serviceResult;
        }

        public async Task<IServiceResult> DeleteAsync(string username)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                var profile =
                    _context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile != null) //delete the user's profile before deleting the user
                {
                    _context.CustomerProfiles.Remove(profile);
                    _context.SaveChanges();
                }
                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public Task<IServiceResult> ResetPasswordByMail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> ResetPasswordByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        private async Task AssignRole(ApplicationUser user, string role)
        {
            if (user == null)
                throw new InvalidArgumentException("The argument user is null");
            if (String.IsNullOrEmpty(role))
                throw new InvalidArgumentException("The argument role is null or empty");
            if (!await _roleManager.RoleExistsAsync(role))
                throw new InvalidArgumentException("The role doese not exist");

            var existingRoles = await _userManager.GetRolesAsync(user);
            if (!existingRoles.Contains(role))
                await _userManager.AddToRoleAsync(user, role);
        }
    }
}
