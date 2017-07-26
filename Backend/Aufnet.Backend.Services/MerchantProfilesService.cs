using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Services.Base;
using Aufnet.Backend.Services.Base.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services
{
    public class MerchantProfilesService : IMerchantProfileService
    {
        private readonly ApplicationDbContext _context;

        public MerchantProfilesService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IServiceResult> CreateProfile(MerchantProfileDto merchantProfileDto)
        {
            var serviceResult = new ServiceResult();
            if (String.IsNullOrEmpty(merchantProfileDto.BusinessName))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "BusinessName"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(merchantProfileDto.AddressDto.City))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "City"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(merchantProfileDto.AddressDto.Country))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "Country"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(merchantProfileDto.AddressDto.Detail))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "Detail"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(merchantProfileDto.AddressDto.State))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "State"));
                return serviceResult;
            }

            if (String.IsNullOrEmpty(merchantProfileDto.AddressDto.PostCode))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "State"));
                return serviceResult;
            }
            var merchantProfile = new MerchantProfile
            {
                Address = new Address
                {
                    City = merchantProfileDto.AddressDto.City,
                    Country = merchantProfileDto.AddressDto.Country,
                    PostCode = merchantProfileDto.AddressDto.PostCode,
                    Detail = merchantProfileDto.AddressDto.Detail,
                    State = merchantProfileDto.AddressDto.State
                },
                BusinessName = merchantProfileDto.BusinessName
            };
            
            try
            {
                var result = _context.MerchantProfiles.Add(merchantProfile);
            }
            catch (InvalidArgumentException exception)
            {
                serviceResult.AddError(new ErrorMessage("", exception.Message));
                return serviceResult;
            }

            return serviceResult;
        }

        public async Task<IServiceResult> UpdateProfile(string username, MerchantProfileDto newMerchantProfileDto)
        {
            var serviceResult = new ServiceResult();

            //validation
            if (String.IsNullOrEmpty(username))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "username"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(newMerchantProfileDto.BusinessName))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "BusinessName"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(newMerchantProfileDto.AddressDto.City))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "City"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(newMerchantProfileDto.AddressDto.Country))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "Country"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(newMerchantProfileDto.AddressDto.Detail))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "Detail"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(newMerchantProfileDto.AddressDto.State))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "State"));
                return serviceResult;
            }

            if (String.IsNullOrEmpty(newMerchantProfileDto.AddressDto.PostCode))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "State"));
                return serviceResult;
            }
            var oldmerchantProfile =
                _context.MerchantProfiles.Include(m => m.ApplicationUser)
                    .FirstOrDefault(m => m.ApplicationUser.UserName == username);
            oldmerchantProfile.Address.City = newMerchantProfileDto.AddressDto.City;
            oldmerchantProfile.Address.Country = newMerchantProfileDto.AddressDto.Country;
            oldmerchantProfile.Address.PostCode = newMerchantProfileDto.AddressDto.PostCode;
            oldmerchantProfile.Address.Detail = newMerchantProfileDto.AddressDto.Detail;
            oldmerchantProfile.Address.State = newMerchantProfileDto.AddressDto.State;
            oldmerchantProfile.BusinessName = newMerchantProfileDto.BusinessName;
            
            try
            {
                var result = _context.MerchantProfiles.Update(oldmerchantProfile);
            }
            catch (InvalidArgumentException exception)
            {
                serviceResult.AddError(new ErrorMessage("", exception.Message));
                return serviceResult;
            }
            
            return serviceResult;
        }

        public async Task<IServiceResult> DelteProfile(string username)
        {
            var serviceResult = new ServiceResult();
            if (String.IsNullOrEmpty(username))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "username"));
                return serviceResult;
            }
            var merchantProfile =
                _context.MerchantProfiles.Include(m => m.ApplicationUser)
                    .FirstOrDefault(m => m.ApplicationUser.UserName == username);
            try
            {
                var result = _context.MerchantProfiles.Remove(merchantProfile);
            }
            catch (InvalidArgumentException exception)
            {
                serviceResult.AddError(new ErrorMessage("", exception.Message));
                return serviceResult;
            }

            return serviceResult;
        }
    }
}
