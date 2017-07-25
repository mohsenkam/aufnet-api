using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Base.Exceptions;

namespace Aufnet.Backend.Services
{
    public class CustomersService: ICustomerService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomersService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IServiceResult> SignUpAsync(CustomerSignUpDto value)
        {
            
            var serviceResult = new ServiceResult();
            if (!RolesConstants.Roles.Contains(value.Role))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingRole.Code, ErrorCodesConstants.NotExistingRole.Message));
                return serviceResult;
            }

            var user = new ApplicationUser
            {
                UserName = value.Username,
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
            try
            {
                await AssignRole(user, value.Role);
            }
            catch (InvalidArgumentException exception)
            {
                serviceResult.AddError(new ErrorMessage("", exception.Message));
                return serviceResult;
            }

            return serviceResult;

        }

        public async Task<IServiceResult> ChangePasswordAsync(CustomerChangePasswordDto value)
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

            var user = await _userManager.FindByNameAsync(value.Username);
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

        public IServiceResult SignIn(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IServiceResult ResetPasswordByMail(string email)
        {
            throw new NotImplementedException();
        }

        public IServiceResult ResetPasswordByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task AssignRole(ApplicationUser user, string role)
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
