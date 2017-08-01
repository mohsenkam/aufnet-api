using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities;
using Aufnet.Backend.Data.Models.Entities.Event;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Base.Exceptions;
using Aufnet.Backend.Services.Shared;


namespace Aufnet.Backend.Services
{
    public class MerchantEventsService : IMerchantEventsService
    {
        private readonly ApplicationDbContext _context;
        //private readonly IEmailService _emailService;
        //private readonly UserManager<ApplicationUser> _userManager;

        public MerchantEventsService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IEmailService emailService)
        {
            //_userManager = userManager;
            _context = context;
            //_emailService = emailService;
        }

        public  Task<IServiceResult> DeleteAsync(string username)
        {
            var serviceResult = new ServiceResult();
            try
            {
                //var user = await _userManager.FindByNameAsync(username);
                //if (user == null) //There is no such a user
                //{
                //    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                //        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                //    return serviceResult;
                //}
                //var profile =
                    //_context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                //if (profile != null) //delete the user's profile before deleting the user
                //{
                //    _context.CustomerProfiles.Remove(profile);
                //    _context.SaveChanges();
                //}
                //await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return null;//serviceResult;
        }

        public async Task<IServiceResult> CreateEvent(MerchantEventsDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                //var user = await _userManager.FindByNameAsync(username);
                await _context.MerchantEventses.AddAsync(new MerchantEvents()
                {
                    Title = value.Title,
                    StarDate = value.StarDate,
                    EndDate = value.EndDate
                    //ApplicationUser = user,
                    //ApplicationUserId = user.Id
                });
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }


    }
}
