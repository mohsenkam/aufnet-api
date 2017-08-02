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
    [Route("api/merchants/{username}/event")]
    public class MerchantEventControlle : BaseController
    {
        private readonly IMerchantEventsService _merchantEventControlle;

        public MerchantEventControlle(IMerchantEventsService merchantEventService)
        {
            _merchantEventControlle = merchantEventService;
        }

        // GET api/merchants/john/event
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username)
        {
            //logic
            var result = await _merchantEventControlle.GetEvents(username);
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

        // POST api/merchants/john/event
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post(string username, [FromBody]MerchantEventsDto value)
        {
            var result = await _merchantEventControlle.AddEvent(username, value);
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

        // PUT api/merchants/john/event
        [HttpPut]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Put(string username, [FromBody]MerchantEventsDto value)
        {
            var result = await _merchantEventControlle.UpdateEvent(username, value);
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
