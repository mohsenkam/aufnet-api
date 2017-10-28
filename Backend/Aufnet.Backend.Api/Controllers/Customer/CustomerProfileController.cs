using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers.Customer
{
    [Route("api/customers/{username}/profile")]
    public class CustomerProfileController: BaseController
    {
        //    private readonly ICustomerProfileService _customerProfileService;

        //    public CustomerProfileController(ICustomerProfileService customerProfileService)
        //    {
        //        _customerProfileService = customerProfileService;
        //    }

        //    // GET api/customers/john/profile
        //    [HttpGet]
        //    public async Task<IActionResult> GetAsync(string username)
        //    {
        //        //logic
        //        var result = await _customerProfileService.GetProfileAsync(username);
        //        if (result.HasError())
        //        {
        //            foreach (var error in result.GetResult().GetErrors())
        //            {
        //                ModelState.AddModelError(error.Code, error.Message);
        //            }

        //            return new ValidationFailedResult(ModelState);
        //        }

        //        return Ok(result.GetData());
        //    }

        //    // POST api/customers/john/profile
        //    [HttpPost]
        //    public async Task<IActionResult> Post(string username, [FromBody]CustomerProfileDto value)
        //    {
        //        var result = await _customerProfileService.CreateProfile(username, value);
        //        if (result.HasError())
        //        {
        //            foreach (var error in result.GetErrors())
        //            {
        //                ModelState.AddModelError(error.Code, error.Message);
        //            }

        //            return new ValidationFailedResult(ModelState);
        //        }
        //        return Ok();
        //    }

        //    // PUT api/customers/john/profile
        //    [HttpPut]
        //    public async Task<IActionResult> Put(string username, [FromBody]CustomerProfileDto value)
        //    {
        //        var result = await _customerProfileService.UpdateProfile(username, value);
        //        if (result.HasError())
        //        {
        //            foreach (var error in result.GetErrors())
        //            {
        //                ModelState.AddModelError(error.Code, error.Message);
        //            }

        //            return new ValidationFailedResult(ModelState);
        //        }
        //        return Ok();
        //    }

        //    [HttpDelete]
        //    public async Task<IActionResult> Delete(string username)
        //    {
        //        var result = await _customerProfileService.DelteProfile(username);
        //        if (result.HasError())
        //        {
        //            foreach (var error in result.GetErrors())
        //            {
        //                ModelState.AddModelError(error.Code, error.Message);
        //            }

        //            return new ValidationFailedResult(ModelState);
        //        }
        //        return Ok();
        //    }
    }
}
