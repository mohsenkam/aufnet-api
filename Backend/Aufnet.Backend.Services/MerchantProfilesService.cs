using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Services.Base;
using Aufnet.Backend.Services.Base.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services
{
    public class MerchantProfilesService : IMerchantProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantProfilesService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IGetServiceResult<MerchantProfileDto>> GetProfileAsync(string username)
        {
            var serviceResult = new ServiceResult();
            //validatio
            var getResult = new GetServiceResult<MerchantProfileDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var profile = _context.MerchantProfiles.Include(m=>m.Address).FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));

            MerchantProfileDto mpDto;
            if (profile == null)
                mpDto = null;
            else
            {
                mpDto = new MerchantProfileDto()
                {
                    AddressDto = new AddressDto
                    {
                        City = profile.Address.City,
                        Country = profile.Address.Country,
                        Detail = profile.Address.Detail,
                        PostCode = profile.Address.PostCode,
                        State = profile.Address.State,
                        
                    },
                    LocationDto = new PointDto()
                    {
                        Longitude = profile.Location.Longitude,
                        Latitude = profile.Location.Latitude
                    },
                    BusinessName = profile.BusinessName,
                    
                    //Gender todo: put in the proper place
                };
            }
            getResult.SetData(mpDto);
            return getResult;
        }

        public async Task<IServiceResult> CreateProfile(string username, MerchantProfileDto value)
        {
            var serviceResult = new ServiceResult();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));

                return serviceResult;
            }
            try
            {
                await _context.MerchantProfiles.AddAsync(new MerchantProfile()
                {
                    Address = new Address
                    {
                        City = value.AddressDto.City,
                        Country = value.AddressDto.Country,
                        Detail = value.AddressDto.Detail,
                        PostCode = value.AddressDto.PostCode,
                        State = value.AddressDto.State
                    },
                    BusinessName = value.BusinessName,
                    Location = new Point()
                    {
                        Longitude = value.LocationDto.Longitude,
                        Latitude = value.LocationDto.Latitude
                    },
                    ApplicationUserId = user.Id,
                    ApplicationUser = user
                });
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> UpdateProfile(string username, MerchantProfileDto value)
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
                    _context.MerchantProfiles.Include(m => m.ApplicationUser).Include(m=>m.Address).FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                profile.BusinessName = value.BusinessName;
                profile.Address.City = value.AddressDto.City;
                profile.Address.Country = value.AddressDto.Country;
                profile.Address.Detail = value.AddressDto.Detail;
                profile.Address.PostCode = value.AddressDto.PostCode;
                profile.Address.State = value.AddressDto.State;
                profile.Location.Latitude = value.LocationDto.Latitude;
                profile.Location.Longitude = value.LocationDto.Longitude;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> DeleteProfile(string username)
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
                    _context.MerchantProfiles.Include(mp=>mp.Address).FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                if (profile.Address != null)
                {
                    _context.Addresses.Remove(profile.Address);
                }
                _context.MerchantProfiles.Remove(profile);
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
