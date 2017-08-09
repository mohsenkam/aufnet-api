using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
//using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/[controller]")]    
    public class RegionsController : BaseController
    {
        private readonly IRegionService _regionService;

        public RegionsController(UserManager<ApplicationUser> userManager, IRegionService regionService)
        {
            _regionService = regionService;
        }

        // GET api/regions/john
        [HttpGet("{name}")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(string name)
        {
            //logic
            var result = await _regionService.GetRegionAsync(name);
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


        //POST api/regions
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RegionDto value)
        {
            var result=await _regionService.CreateRegion(value);
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

        //PUT api/regions/john
        [HttpPut("{name}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string name, [FromBody]RegionDto value)
        {
            var result = await _regionService.UpdateRegion(name, value);
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

        // DELETE api/regions/john
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var result = await _regionService.DeleteRegion(name);
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
