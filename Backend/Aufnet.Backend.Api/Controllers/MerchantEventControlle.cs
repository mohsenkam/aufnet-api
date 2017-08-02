using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/merchants/{username}/event")]
    public class MerchantEventControlle : BaseController
    {
        private readonly IMerchantEventsService _merchantEventService;

        public MerchantEventControlle(IMerchantEventsService merchantEventService)
        {
            _merchantEventService = merchantEventService;
        }

        // GET api/merchants/john/event
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username)
        {
            //logic
            var result = await _merchantEventService.GetEvents(username);
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
            var result = await _merchantEventService.CreateEvent(username, value);
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
            var result = await _merchantEventService.UpdateEvent(username, value);
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

        // Delete api/customers/john
        [HttpDelete("{merchantEventId}")]
        public async Task<IActionResult> Delete(string username, int merchantEventId)
        {
            var result = await _merchantEventService.DeleteEvent(username, merchantEventId);
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
