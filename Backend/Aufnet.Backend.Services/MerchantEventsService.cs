using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.Data.Context;
using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Microsoft.EntityFrameworkCore;


namespace Aufnet.Backend.Services
{
    public class MerchantEventsService : IMerchantEventsService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantEventsService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IGetServiceResult<MerchantEventsDto>> GetEventAsync(string username)
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
                    //MerchantProductDto = new MerchantProductDto
                    //{
                    //    //ProductName = merchantevents.MerchantProduct.ProductName,
                    //    //Description = merchantevents.MerchantProduct.Description,
                    //    //IsAvailable = merchantevents.MerchantProduct.IsAvailable,
                    //    //Discount = merchantevents.MerchantProduct.Discount,
                    //    //ApplicationUserId = merchantevents.MerchantProduct.ApplicationUserId
                    //},
                    Id = (int)  merchantevents.Id,
                    Title = merchantevents.Title,
                    Description = merchantevents.Description,
                    StarDate = merchantevents.StarDate,
                    EndDate = merchantevents.EndDate,
                    MerchantUserName = merchantevents.ApplicationUser.UserName
                };
            }
            getResult.SetData(meDto);
            return getResult;
        }

        public async Task<IServiceResult> DeleteEvent(string username, int merchantEventId)
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
                else
                {
                    var merchantEvent = _context.MerchantEvents.FirstOrDefault(me => me.Id == merchantEventId);
                    if (merchantEvent == null)
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidArgument.Code,
                            ErrorCodesConstants.InvalidArgument.Message + "event doesn't exist"));
                        return serviceResult;
                    }
                    _context.MerchantEvents.Remove(merchantEvent);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> CreateEvent(string username, MerchantEventsDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                        ErrorCodesConstants.NotExistingUser.Message));

                    return serviceResult;
                }
                await _context.MerchantEvents.AddAsync(new MerchantEvent()
                {
                    //MerchantProducts = new MerchantProduct
                    //{
                    //    ProductName = value.MerchantProductDto.ProductName,
                    //    Description = value.MerchantProductDto.Description,
                    //    IsAvailable = value.MerchantProductDto.IsAvailable,
                    //    Discount = value.MerchantProductDto.Discount,
                    //    //Id = value.MerchantProductDto.Id
                    //},
                    Title = value.Title,
                    Description = value.Description,
                    StarDate = value.StarDate,
                    EndDate = value.EndDate,
                    //
                    ApplicationUser = user,
                    ApplicationUserId = user.Id
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        //public async Task<IServiceResult> UpdateFristEvent(string username, MerchantEventsDto value)
        //{
        //    var serviceResult = new ServiceResult();
        //    try
        //    {
        //        var user = await _userManager.FindByNameAsync(username);
        //        if (user == null) //There is no such a user
        //        {
        //            serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
        //                ErrorCodesConstants.NotExistingUser.Message));
        //            return serviceResult;
        //        }
        //        var mevent =
        //            _context.MerchantEvents.FirstOrDefault(me => me.ApplicationUser.UserName.Equals(username));
        //        if (mevent == null) //there is no event for this user
        //        {
        //            serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
        //                ErrorCodesConstants.ManipulatingMissingEntity.Message));
        //            return serviceResult;
        //        }

        //        mevent.Title = value.Title;
        //        mevent.Description = value.Description;
        //        mevent.StarDate = value.StarDate;
        //        mevent.EndDate = value.EndDate;
        //        //mevent.MerchantProduct.Description =  value.MerchantProductDto.Description;
        //        //mevent.MerchantProduct.IsAvailable = value.MerchantProductDto.IsAvailable;
        //        //mevent.MerchantProduct.Discount = value.MerchantProductDto.Discount;

        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResult.AddError(new ErrorMessage("", ex.Message));
        //    }
        //    return serviceResult;
        //}

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
                var mevent =
                    _context.MerchantEvents.FirstOrDefault(me => me.ApplicationUser.UserName.Equals(username));
                if (mevent == null) //there is no event for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                else
                {
                    var merchantEvent = _context.MerchantEvents.FirstOrDefault(me => me.Id == value.Id);
                    if (merchantEvent == null)
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidArgument.Code,
                            ErrorCodesConstants.InvalidArgument.Message + "event doesn't exist"));
                        return serviceResult;
                    }
                    mevent.Title = value.Title;
                    mevent.Description = value.Description;
                    mevent.StarDate = value.StarDate;
                    mevent.EndDate = value.EndDate;
                    //mevent.MerchantProduct.Description =  value.MerchantProductDto.Description;
                    //mevent.MerchantProduct.IsAvailable = value.MerchantProductDto.IsAvailable;
                    //mevent.MerchantProduct.Discount = value.MerchantProductDto.Discount;

                    _context.Update(mevent);
                }
               
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IGetServiceResult<List<MerchantEventsDto>>> SearchMerchantEvents(DateTime startDate, DateTime endDate)
        {
            var getResult = new GetServiceResult<List<MerchantEventsDto>>();
            var filteredEvents =  _context.MerchantEvents.Where(me => me.StarDate >= startDate && me.EndDate <= endDate);
            //...

            List<MerchantEventsDto> meDtos;

            meDtos = filteredEvents.Select(me => new MerchantEventsDto()
            {
                Title = me.Title,
                Description = me.Description,
                StarDate = me.StarDate,
                EndDate = me.EndDate,
            }).ToList();

            getResult.SetData(meDtos);
            return getResult;


        }

    }
}