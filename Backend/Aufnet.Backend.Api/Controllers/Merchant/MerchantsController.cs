using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Aufnet.Backend.Api.Shared;

namespace Aufnet.Backend.Api.Controllers.Merchant
{
    [Route("api/[controller]")]
    public class MerchantsController : BaseController
    {
        private readonly IMerchantService _merchantService;

        public MerchantsController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        //POST api/merchants/searchbyaddress
        [HttpPost("searchbyaddress")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> SearchByAddress([FromBody]AddressDto addressDto)
        {
            //logic
            var result = await _merchantService.SearchMerchantByAddress(addressDto);
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


        //POST api/merchants/create
        [HttpPost("create")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> CreateContract( [FromBody]MerchantCreateDto value )
        {
            var result = await _merchantService.CreateAccountAsync(value);
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

        //POST api/merchants/upload
        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadLogo( IFormFile file )
        {
            var result = await _merchantService.SaveLogoAsync(file, Request.Headers);

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


        public async Task<IActionResult> SendRegistrationEmail(string trackingId)
        {
            var result = await _merchantService.SendRegistrationEmailAsync(trackingId);

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

        //GET api/merchants
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(PagingParams pagingParams)
        {
            var result = await _merchantService.GetMerchantsContractsSummaryAsync(pagingParams);

            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok(
                new
                {
                    data = result.GetData(),
                    totalCount = result.GetTotalCount()
                });
        }

        //POST api/merchants
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]MerchantSignUpDto value)
        {
            var result = await _merchantService.SignUpAsync(value);
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


        //PUT api/merchants/confirmemail
        [HttpPost("confirmemail")]
        [ValidateModel]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailDto value)
        {
            var result = await _merchantService.ConfirmEmailAsync(value);
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


        //PUT api/merchants/john
        [HttpPut("{username}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePassword(string username, [FromBody]MerchantChangePasswordDto value)
        {
            var result = await _merchantService.ChangePasswordAsync(username, value);
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

        // DELETE api/merchants/john
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var result = await _merchantService.DeleteAsync(username);
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
