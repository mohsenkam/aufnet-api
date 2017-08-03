using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/merchants/{username}/product")]
    public class MerchantProductController : BaseController
    {
        private readonly IMerchantProductService _merchantProductService;

        public MerchantProductController(IMerchantProductService merchantProductService)
        {
            _merchantProductService = merchantProductService;
        }

        // GET api/merchants/john/product
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username)
        {
            //logic
            var result = await _merchantProductService.GetProductAsync(username);
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

        // POST api/merchants/john/product
        [HttpPost]
        public async Task<IActionResult> Post(string username, [FromBody]MerchantProductDto value)
        {
            var result = await _merchantProductService.CreateProduct(username, value);
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

        // PUT api/merchants/john/product
        [HttpPut]
        public async Task<IActionResult> Put(string username, [FromBody]MerchantProductDto value)
        {
            var result = await _merchantProductService.UpdateProduct(username, value);
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
        
        [HttpDelete]
        public async Task<IActionResult> Delete(string username)
        {
            var result = await _merchantProductService.DelteProduct(username);
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
