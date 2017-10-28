using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.Services.Merchants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Aufnet.Backend.Api.Shared;

namespace Aufnet.Backend.Api.Controllers.Merchant
{
    [Route("api/merchants/user")]
    public class MerchantsUserController : BaseController
    {
        private readonly IMerchantUserService _merchantUserService;

        public MerchantsUserController(IMerchantUserService merchantUserService)
        {
            _merchantUserService = merchantUserService;
        }


        //POST api/merchants/user
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post( [FromBody]MerchantSignUpDto value )
        {
            var result = await _merchantUserService.SignUpAsync(value);
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
        //public async Task<IActionResult> SendRegistrationEmail(string trackingId)
        //{
        //    var result = await _merchantUserService.SendRegistrationEmailAsync(trackingId);

        //    if (result.HasError())
        //    {
        //        foreach (var error in result.GetErrors())
        //        {
        //            ModelState.AddModelError(error.Code, error.Message);
        //        }

        //        return new ValidationFailedResult(ModelState);
        //    }
        //    return Ok(result.GetExteraData());
        //}

        


        //PUT api/merchants/user/confirmemail
        [HttpPost("confirmemail")]
        [ValidateModel]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailDto value)
        {
            var result = await _merchantUserService.ConfirmEmailAsync(value);
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


        //PUT api/merchants/user/john
        [HttpPut("{username}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePassword(string username, [FromBody]MerchantChangePasswordDto value)
        {
            var result = await _merchantUserService.ChangePasswordAsync(username, value);
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
