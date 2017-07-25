using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Base.Exceptions;
using Aufnet.Backend.Services.Shared;

namespace Aufnet.Backend.Services
{
    public class CustomerProfilesService : ICustomerProfileService
    {
        private readonly ApplicationDbContext _context;

        public CustomerProfilesService(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<IServiceResult> SignUpAsync(string username, string password, string role)
        //{
        //    var serviceResult = new ServiceResult();
        //    var user = new ApplicationUser
        //    {
        //        UserName = username,
        //    };
        //    var result = await _userManager.CreateAsync(user, password);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            serviceResult.AddError(new ErrorMessage("", error.Description));
        //        }

        //        return serviceResult;
        //    }
        //    try
        //    {
        //        await AssignRole(user, role);
        //    }
        //    catch (InvalidArgumentException exception)
        //    {
        //        serviceResult.AddError(new ErrorMessage("", exception.Message));
        //        return serviceResult;
        //    }

        //    return serviceResult;

        //}


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
