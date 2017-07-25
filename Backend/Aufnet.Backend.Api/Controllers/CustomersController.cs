using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/[controller]")]    
    public class CustomersController: BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerService _customerService;

        public CustomersController(UserManager<ApplicationUser> userManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _customerService = customerService;
        }


        //POST 
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]CustomerSignUpDto value)
        {
            //validation
            if (!RolesConstants.Roles.Contains(value.Role))
            {
                ModelState.AddModelError(ErrorCodesConstants.NotExistingRole.Code, ErrorCodesConstants.NotExistingRole.Message);
                return new ValidationFailedResult(ModelState);
            }

            //preparation

            //logic
            var result=await _customerService.SignUpAsync(value.Username, value.Password, value.Role);
            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok();
        }

        //POST 
        [HttpPost("{username}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePassword(string username, [FromBody]CustomerChangePasswordDto value)
        {

            //validation
            if (String.IsNullOrEmpty(value.CurrentPassword))
            {
                ModelState.AddModelError(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "CurrentPassword");
                return new ValidationFailedResult(ModelState);
            }
            if (String.IsNullOrEmpty(value.NewPassword))
            {
                ModelState.AddModelError(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "NewPassword");
                return new ValidationFailedResult(ModelState);
            }

            //preparation

            //logic
            var result = await _customerService.SignUpAsync(username, value.CurrentPassword, value.NewPassword);
            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            
            return Ok();
        }

        


        // DELETE customers/5
        [HttpDelete("{username}")]
        public async Task Delete(string username)
        {
            await _userManager.DeleteAsync(await _userManager.FindByNameAsync(username));
        }


        

    }
}
