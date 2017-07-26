using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/merchants/{username}/profile")]
    public class MerchantProfileControlle : BaseController
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMerchantProfileService _merchantProfileService;

        public MerchantProfileControlle(IMerchantProfileService merchantProfileService)
        {
            _merchantProfileService = merchantProfileService;
        }

        // GET api/merchants/golnar/merchantprofile

        [HttpGet]
        public IEnumerable<string> Get(string username)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]MerchantProfileDto merchantProfileDto)
        {
            var result = await _merchantProfileService.CreateProfile(merchantProfileDto);
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
        

        // PUT api/merchants/golnar/merchantprofile
        [HttpPut]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Put(string username, [FromBody]MerchantProfileDto merchantProfileDto)
        {
            var result = await _merchantProfileService.UpdateProfile(username, merchantProfileDto);
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

        [HttpDelete("{username}")]
        public async Task Delete(string username)
        {
            await _merchantProfileService.DelteProfile(username);
        }
    }
}
