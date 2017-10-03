using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.Services;
using Aufnet.Backend.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers.Merchant
{
    [Route("api/merchants/{username}/products")]
    public class ProductController : BaseController
    {
        private readonly IMerchantProductService _merchantProductService;

        public ProductController(IMerchantProductService merchantProductService)
        {
            _merchantProductService = merchantProductService;
        }

        // GET api/merchants/john/products
        [HttpGet]
        public async Task<IActionResult> GetAll(string username)
        {
            var result = await _merchantProductService.GetProductsAsync(username);
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

        // POST api/merchants/john/products
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

        // PUT api/merchants/john/products
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
        public async Task<IActionResult> Delete(string username, long productId)
        {
            var result = await _merchantProductService.DelteProduct(username, productId);
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
