using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/customers/{username}/calendar")]
    public class CustomersCalendarController: BaseController
    {
        private readonly ICustomerCalendarService _customerCalendarService;

        public CustomersCalendarController(ICustomerCalendarService customerCalendarService)
        {
            _customerCalendarService = customerCalendarService;
        }

        // GET api/customers/john/calendar
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username)
        {
            var result = await _customerCalendarService.GetEventsAsync(username);
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

        // POST api/customers/john/calendar/1
        [HttpPost("{merchantEventId}")]
        public async Task<IActionResult> Post( string username, int merchantEventId )
        {
            var result = await _customerCalendarService.AddEventBookmarkAsync(username, merchantEventId);
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

        // PUT api/customers/john/calendar/1
        [HttpDelete("{merchantEventId}")]
        public async Task<IActionResult> Delete(string username, int merchantEventId)
        {
            var result = await _customerCalendarService.RemoveEventBookmarkAsync(username, merchantEventId);
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
