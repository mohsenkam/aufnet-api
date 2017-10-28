using System;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Shared.Exceptions;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Aufnet.Backend.Data.Models.Entities.Customers;
using Aufnet.Backend.Data.Repository;
using Aufnet.Backend.ApiServiceShared;

//using Aufnet.Backend.Api.Shared;


namespace Aufnet.Backend.Services.Customers
{
    public class CustomerUserService: ICustomerUserService
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IRepository<Customer> _cusRepository;
        private readonly Config _config;

        public CustomerUserService( IRepository<Customer> cusRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, Config config )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _cusRepository = cusRepository;
            _config = config;
        }

        
        public async Task<IServiceResult> SignUpAsync(CustomerSignUpDto value)
        {
            
            var serviceResult = new ServiceResult();

            // Check if the use already exists! Make sure to use UserManager, not the Merchant, to check if the user exists.
            ApplicationUser existingUser = await _userManager.FindByEmailAsync(value.Email);

            if (existingUser != null)
            {
                // The user already exists
                if (await _userManager.IsEmailConfirmedAsync(existingUser))
                {
                    // The user has confirmed his/her email
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.AddingDuplicateEntry.Code,
                        ErrorCodesConstants.AddingDuplicateEntry.Message));
                    return serviceResult;
                }
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EmailNotConfirmed.Code,
                    ErrorCodesConstants.EmailNotConfirmed.Message));
                return serviceResult;
            }

            // the user doesn't exist
            var user = new ApplicationUser
            {
                UserName = value.Email,
                Email = value.Email,
            };

            // Cusetmer is created before actually creating the user. This prevents having dangling Users.
            var customer = new Customer()
            {
                User = user
            };
            await _cusRepository.AddAsync(customer);


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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = _config.BaseUrl + "/auth/confirmemail?userId=" + user.Id + "&code="+token;

            await _emailService.SendEmailAsync(new EmailModel
            {
                ToEmail = value.Email,
                Subject = "Complete your registration",
                Body = $"Follow the link to activate your account: </br>{callbackUrl}"
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
            catch (InvalidArgumentException ex)
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

            //validation todo: is this needed?
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
                var customer = await _cusRepository.Query(c => c.User.UserName == username).FirstOrDefaultAsync();
                if (customer == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                }
                
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                // todo: Should the profile be deleted manually?
                //var profile =
                //   await _context.CustomerProfiles.FirstOrDefaultAsync(cp => cp.Customer.User.UserName.Equals(username));
                //if (profile != null) //delete the user's profile before deleting the user
                //{
                //    _context.CustomerProfiles.Remove(profile);
                //    _context.SaveChanges();
                //}
                await _userManager.DeleteAsync(user);

                // Delete the Customer here to avoid dangling users
                await _cusRepository.DeleteAsync(customer);
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
