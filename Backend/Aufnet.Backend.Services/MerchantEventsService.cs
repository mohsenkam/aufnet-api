using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities;
using Aufnet.Backend.Data.Models.Entities.Event;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Services.Base.Exceptions;
using Aufnet.Backend.Services.Shared;


namespace Aufnet.Backend.Services
{
    public class MerchantEventsService : IMerchantEventsService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantEventsService(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            IEmailService emailService)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IGetServiceResult<MerchantEventsDto>> GetEvents(string username)
        {
            var serviceResult = new ServiceResult();

            //validation
            var getResult = new GetServiceResult<MerchantEventsDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var merchantevents = _context.MerchantEvents.FirstOrDefault(me => me.ApplicationUser.UserName.Equals(username));

            MerchantEventsDto meDto;
            if (merchantevents == null)
                meDto = null;
            else
            {
                meDto = new MerchantEventsDto()
                {
                    MerchantProductDto =  new MerchantProductDto
                    {
                        ProductName = merchantevents.Title,
                        Description = merchantevents.Description,
                        //ApplicationUserId = merchantevents.Product.ApplicationUserId
                    },
                    Title = merchantevents.Title,

                    //Gender todo: put in the proper place
                };
            }
            getResult.SetData(meDto);
            return getResult;
        }

        public async Task<IServiceResult> RemoveEvent(string username, int merchantEventId)
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
                var mevant =
                    _context.MerchantEvents.FirstOrDefault(me => me.ApplicationUser.UserName.Equals(username));
                if (mevant == null) //there is no event for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                _context.MerchantEvents.Remove(mevant);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> AddEvent(string username, MerchantEventsDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                await _context.MerchantEvents.AddAsync(new MerchantEvents()
                {
                    Title = value.Title,
                    Description = value.Description,
                    StarDate = value.StarDate,
                    EndDate = value.EndDate,
                    //
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

        public async Task<IServiceResult> UpdateEvent(string username, MerchantEventsDto value)
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
                //var mevent =
                //    _context.MerchantEvents.Include(p => p.Product).FirstOrDefault(me => me.ApplicationUser.UserName.Equals(username));
                //if (mevent == null) //there is no event for this user
                //{
                //    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                //        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                //    return serviceResult;
                //}

                //mevent..pro.City = value.AddressDto.City;
                //mevent.Address.Country = value.AddressDto.Country;
                //mevent.Address.Detail = value.AddressDto.Detail;
                //mevent.Address.PostCode = value.AddressDto.PostCode;
                //mevent.Address.State = value.AddressDto.State;
                //_context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

    }
}