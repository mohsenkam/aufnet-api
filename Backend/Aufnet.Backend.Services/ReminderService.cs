using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Models.Reminder;
using Aufnet.Backend.Data.Context;
using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Reminder;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services
{
    public class ReminderService : IReminderService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReminderService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IServiceResult> CreateReminder(string username, ReminderDto value)
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
                await _context.Reminders.AddAsync(new Reminder()
                {
                    TrigerDateTime = value.TrigerDateTime,
                    Event = value.Event,
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

        public async Task<IServiceResult> DeleteReminder(string username, int merchantReminderId)
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
                    _context.Reminders.FirstOrDefault(me => me.ApplicationUser.UserName.Equals(username));
                if (mevant == null) //there is no event for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                else
                {
                    var merchantReminder = _context.Reminders.FirstOrDefault(mr => mr.Id == merchantReminderId);
                    if (merchantReminder == null)
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidArgument.Code,
                            ErrorCodesConstants.InvalidArgument.Message + "reminder doesn't exist"));
                        return serviceResult;
                    }
                    _context.Reminders.Remove(merchantReminder);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> UpdateReminder(string username, ReminderDto value)
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
                var mReminder =
                    _context.Reminders.Include(m => m.ApplicationUser)
                        .Where(m => m.Id == value.Id)
                        .FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (mReminder == null) //there is no event for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                mReminder.TrigerDateTime = value.TrigerDateTime;
                mReminder.Event = value.Event;
                //
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