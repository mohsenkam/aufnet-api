using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Services.Admin.Merchants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//using Aufnet.Backend.Api.Shared;

namespace Aufnet.Backend.Api.Controllers.Admin.Merchants
{
    [Route("api/merchants")]
    public class MerchantsContractController : BaseController
    {
        private readonly IAdminContractService _adminContractService;

        public MerchantsContractController(IAdminContractService adminContractService)
        {
            _adminContractService = adminContractService;
        }



        //POST api/merchants/contract
        [HttpPost("contract")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> CreateContract( [FromBody]MerchantCreateDto value )
        {
            var result = await _adminContractService.CreateContractAsync(value);
            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok(result.GetExteraData());
        }

        //POST api/merchants/{id}/contract/upload
        [HttpPost("{id}/contract/upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadLogo( long id, IFormFile file )
        {
            var result = await _adminContractService.SaveLogoAsync(id, file);

            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok(result.GetExteraData());
        }

    }
}
