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
    public class MerchantEventController : BaseController
    {
        private readonly IMerchantEventsService _merchantEventService;

        public MerchantEventController(IMerchantEventsService merchantEventService)
        {
            _merchantEventService = merchantEventService;
        }

        // GET api/merchants/john/event
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username)
        {
            //logic
            var result = await _merchantEventService.GetEventAsync(username);
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

        //POST api/merchants/SearchMerchantEvents
        [HttpPost("SearchMerchantEvents")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> SearchMerchantEvents([FromBody]MerchantEventsDto merchantEventsDto)
        {
            //logic
            var result = await _merchantEventService.SearchMerchantEvents(merchantEventsDto.StarDate, merchantEventsDto.EndDate);
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
    }
}
