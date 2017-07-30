using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/merchants/{username}/profile")]
    public class MerchantProfileController : BaseController
    {
        private readonly IMerchantProfileService _merchantProfileService;

        public MerchantProfileController(IMerchantProfileService merchantProfileService)
        {
            _merchantProfileService = merchantProfileService;
        }

        // GET api/merchants/john/profile
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username)
        {
            //logic
            var result = await _merchantProfileService.GetProfileAsync(username);
            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok(result.GetData());
        }

        // POST api/merchants/john/profile
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post(string username, [FromBody]MerchantProfileDto value)
        {
            var result = await _merchantProfileService.CreateProfile(value);
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

        // PUT api/merchants/john/profile
        [HttpPut]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Put(string username, [FromBody]MerchantProfileDto value)
        {
            var result = await _merchantProfileService.UpdateProfile(username, value);
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
