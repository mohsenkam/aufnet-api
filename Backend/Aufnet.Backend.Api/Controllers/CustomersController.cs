using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
//using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
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

        //POST api/customers
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]CustomerSignUpDto value)
        {
            var result=await _customerService.SignUpAsync(value);
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

        //PUT api/customers/confirmemail
        [HttpPost("confirmemail")]
        [ValidateModel]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailDto value )
        {
            var result = await _customerService.ConfirmEmailAsync(value);
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


        //PUT api/customers/john
        [HttpPut("{username}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePassword(string username, [FromBody]CustomerChangePasswordDto value)
        {
            var result = await _customerService.ChangePasswordAsync(username, value);
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

        // DELETE api/customers/john
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var result = await _customerService.DeleteAsync(username);
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

    }
}
