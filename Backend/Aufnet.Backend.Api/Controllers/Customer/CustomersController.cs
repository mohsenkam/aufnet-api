using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services;
using Aufnet.Backend.Services.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Aufnet.Backend.Api.Shared;

namespace Aufnet.Backend.Api.Controllers.Customer
{
    [Route("api/[controller]")]    
    public class CustomersController: BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerUserService _customerUserService;

        public CustomersController(UserManager<ApplicationUser> userManager, ICustomerUserService customerUserService)
        {
            _userManager = userManager;
            _customerUserService = customerUserService;
        }

        //POST api/customers
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]CustomerSignUpDto value)
        {
            var result=await _customerUserService.SignUpAsync(value);
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
            var result = await _customerUserService.ConfirmEmailAsync(value);
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
            var result = await _customerUserService.ChangePasswordAsync(username, value);
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
            var result = await _customerUserService.DeleteAsync(username);
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
