using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Reminder;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/reminder")]
    public class ReminderController : BaseController
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }


        // POST api/reminder
        [HttpPost]
        public async Task<IActionResult> Post(string username, [FromBody] ReminderDto value)
        {
            var result = await _reminderService.CreateReminder(username, value);
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

        // PUT api/reminder
        [HttpPut]
        public async Task<IActionResult> Put(string username, [FromBody] ReminderDto value)
        {
            var result = await _reminderService.UpdateReminder(username, value);
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

        // DELETE api/reminder
        [HttpDelete("{reminderId}")]
        public async Task<IActionResult> Delete(string username, int reminderId)
        {
            var result = await _reminderService.DeleteReminder(username, reminderId);
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
