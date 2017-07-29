using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Customer;
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

        public async Task<IServiceResult> CreateProfile(string username, CustomerProfileDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                await _context.CustomerProfiles.AddAsync(new CustomerProfile()
                {
                    FirstName =  value.FirstName,
                    LastName = value.LastName,
                    PhoneNumber = value.PhoneNumber,
                    Email = value.Email,
                    DateOfBirth = value.DateOfBirth,
                    JoiningDate = value.JoiningDate, //Todo: set it here
                    //Gender = value.Gender,
                    ApplicationUser = user,
                    ApplicationUserId = user.Id
                });
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> UpdateProfile(string username, CustomerProfileDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                        ErrorCodesConstants.NotExistingUser.Message));
                    return serviceResult;
                }
                var profile =
                    _context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                profile.FirstName = value.FirstName;
                profile.LastName = value.LastName;
                profile.PhoneNumber = value.PhoneNumber;
                profile.Email = value.Email;
                profile.DateOfBirth = value.DateOfBirth;
                profile.JoiningDate = value.JoiningDate;
                //Gender = value.Gender,
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> DelteProfile(string username)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                        ErrorCodesConstants.NotExistingUser.Message));
                    return serviceResult;
                }
                var profile =
                    _context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                _context.CustomerProfiles.Remove(profile);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }
    }
}
