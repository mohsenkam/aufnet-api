using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
//using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
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
    public class MerchantsController : BaseController
    {
        private readonly IMerchantService _merchantService;

        public MerchantsController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        //POST api/merchants
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]MerchantSignUpDto value)
        {
            var result = await _merchantService.SignUpAsync(value);
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

        //PUT api/merchants/confirmemail
        [HttpPost("confirmemail")]
        [ValidateModel]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailDto value)
        {
            var result = await _merchantService.ConfirmEmailAsync(value);
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


        //PUT api/merchants/john
        [HttpPut("{username}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePassword(string username, [FromBody]MerchantChangePasswordDto value)
        {
            var result = await _merchantService.ChangePasswordAsync(username, value);
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

        // DELETE api/merchants/john
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var result = await _merchantService.DeleteAsync(username);
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
