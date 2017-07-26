using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/customers/{username}/profile")]
    public class CustomerProfileController: BaseController
    {
        private readonly ICustomerProfileService _customerProfileService;

        public CustomerProfileController(ICustomerProfileService customerProfileService)
        {
            _customerProfileService = customerProfileService;
        }

        // GET api/customers/john/customerprofile
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username)
        {
            //logic
            var result = await _customerProfileService.GetProfileAsync(username);
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

        // POST api/customers/john/customerprofile
        [HttpPost]
        public void Post([FromBody]CustomerProfileDto value)
        {
        }

        // PUT api/customers/john/customerprofile
        [HttpPut]
        public void Put(string username, [FromBody]CustomerProfileDto value)
        {
        }
    }
}
