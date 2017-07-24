using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Base.Exceptions;

namespace Aufnet.Backend.Services
{
    public class MerchantsService: IMerchantsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IServiceResult> SignUpAsync(string username, string password, string role)
        {
            var serviceResult = new ServiceResult();
            var user = new ApplicationUser
            {
                UserName = username,
            };
            var result = await _userManager.CreateAsync(user, password);
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
                await AssignRole(user, role);
            }
            catch (InvalidArgumentException exception)
            {
                serviceResult.AddError(new ErrorMessage("", exception.Message));
                return serviceResult;
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
