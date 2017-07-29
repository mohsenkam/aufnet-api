using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities;
using Aufnet.Backend.Data.Models.Entities.Customer;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;

namespace Aufnet.Backend.Services
{
    public class CustomerCalendarService : ICustomerCalendarService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerCalendarService( ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IGetServiceResult<CustomerCalendarDto>> GetEventsAsync(string username)
        {
            var serviceResult = new ServiceResult();

            //validation
            var getResult = new GetServiceResult<CustomerCalendarDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var events = _context.BookmarkedMerchantEvents.FirstOrDefault(bme => bme.Customer.UserName.Equals(username));

            CustomerCalendarDto ccDto;
            if (events == null)
                ccDto = null;
            else
            {
                var eventDtos = events.MerchantEvents.Select(e => new EventDto()
                {
                    Title = e.Title,
                    MerchantUserName = e.Merchant.UserName
                }).ToList();
                ccDto = new CustomerCalendarDto()
                {
                    EventDtos = eventDtos
                };
            }
            getResult.SetData(ccDto);
            return getResult;
        }

        public async Task<IServiceResult> AddEventBookmarkAsync(string username, int merchantEventId )
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

                var bookmarkedEvent = _context.BookmarkedMerchantEvents.FirstOrDefault(bme => bme.Customer.UserName.Equals(username));
                if (bookmarkedEvent == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                else
                {
                    var merchantEvent = _context.MerchantEvents.FirstOrDefault(me => me.Id == merchantEventId);
                    if (merchantEvent == null)
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidArgument.Code,
                            ErrorCodesConstants.InvalidArgument.Message + "event doesn't exist"));
                        return serviceResult;
                    }

                    var count = bookmarkedEvent.MerchantEvents.Count(me => me.Merchant.Id.Equals(merchantEvent.Merchant.Id));
                    if (count > 0) //This event is already bookmarked
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.AddingDuplicateEntry.Code,
                            ErrorCodesConstants.AddingDuplicateEntry.Message));
                        return serviceResult;
                    }
                    bookmarkedEvent.MerchantEvents.Add(merchantEvent);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> RemoveEventBookmarkAsync(string username, int merchantEventId)
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

                var bookmarkedEvent = _context.BookmarkedMerchantEvents.FirstOrDefault(bme => bme.Customer.UserName.Equals(username));
                if (bookmarkedEvent == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                else
                {
                    var merchantEvent = _context.MerchantEvents.FirstOrDefault(me => me.Id == merchantEventId);
                    if (merchantEvent == null)
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidArgument.Code,
                            ErrorCodesConstants.InvalidArgument.Message + "event doesn't exist"));
                        return serviceResult;
                    }

                    var count = bookmarkedEvent.MerchantEvents.Count(me => me.Merchant.Id.Equals(merchantEvent.Merchant.Id));
                    if (count == 0) //This event is not bookmarked
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.AddingDuplicateEntry.Code,
                            ErrorCodesConstants.AddingDuplicateEntry.Message));
                        return serviceResult;
                    }
                    bookmarkedEvent.MerchantEvents.Remove(merchantEvent);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

      
    }
}
