using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;

namespace Aufnet.Backend.Services
{
    public class CustomerProfilesService : ICustomerProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerProfilesService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IGetServiceResult<CustomerProfileDto>> GetProfileAsync(string username)
        {
            var serviceResult = new ServiceResult();

            

            //validatio
            var getResult = new GetServiceResult<CustomerProfileDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var profile = _context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));

            CustomerProfileDto cpDto;
            if (profile == null)
                cpDto = null;
            else
            {
                cpDto = new CustomerProfileDto()
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    DateOfBirth = profile.DateOfBirth,
                    JoiningDate = profile.JoiningDate,
                    Email = profile.Email,
                    PhoneNumber = profile.Email,
                    //Gender todo: put in the proper place
                };
            }
            getResult.SetData(cpDto);
            return getResult;
        }

        public Task<IServiceResult> CreateProfile()
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> UpdateProfile()
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> DelteProfile()
        {
            throw new NotImplementedException();
        }
    }
}
