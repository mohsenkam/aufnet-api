using System;
using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Services.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers.Customer
{
    [Route("api/customers/search")]
    public class CustomerSearchController : BaseController
    {
        private readonly ICustomerSearcheService _customerSearchService;

        public CustomerSearchController(ICustomerSearcheService customerSearchService)
        {
            _customerSearchService = customerSearchService;
        }

        [HttpGet("products/{order}")]
        public async Task<IActionResult> GetProducts(string order, SearchParams searchParams)
        {
            var result = await _customerSearchService.GetProductsAsync(order, searchParams);
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

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var result = await _customerSearchService.GetProductAsync(id);
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

        [HttpGet("merchants/{order}")]
        public async Task<IActionResult> GetMerchants(string order, SearchParams searchParams)
        {
            var result = await _customerSearchService.GetMerchantsAsync(order, searchParams);
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

        [HttpGet("merchants/{id}")]
        public async Task<IActionResult> GetMerchant(long id)
        {
            var result = await _customerSearchService.GetMerchantAsync(id);
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

        [HttpGet("offers/ib/{order}")]
        public async Task<IActionResult> GetItemBasedOffers(string order, SearchParams searchParams)
        {
            var result = await _customerSearchService.GetItemBasedOffersAsync(order, searchParams);
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

        [HttpGet("offers/ib/{id}")]
        public async Task<IActionResult> GetItemBasedOffer(long id)
        {
            var result = await _customerSearchService.GetItemBasedOfferAsync(id);
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

        [HttpGet("offers/qb/{order}")]
        public async Task<IActionResult> GetQuantityBasedOffers(string order, SearchParams searchParams)
        {
            var result = await _customerSearchService.GetQuantityBasedOffersAsync(order, searchParams);
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

        [HttpGet("offers/qb/{id}")]
        public async Task<IActionResult> GetQuantityBasedOffer(long id)
        {
            var result = await _customerSearchService.GetQuantityBasedOfferAsync(id);
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

        [HttpGet("offers/lb/{order}")]
        public async Task<IActionResult> GetLoyaltyBasedOffers(string order, SearchParams searchParams)
        {
            var result = await _customerSearchService.GetLoyaltyBasedOffersAsync(order, searchParams);
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

        [HttpGet("offers/lb/{id}")]
        public async Task<IActionResult> GetLoyaltyBasedOffer(long id)
        {
            var result = await _customerSearchService.GetLoyaltyBasedOfferAsync(id);
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

        [HttpGet("")]
        public async Task<IActionResult> SearchProduct(string term, SearchParams searchParams )
        {
            var result = await _customerSearchService.SearchProductsAsync(searchParams);
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

        [HttpGet]
        public async Task<IActionResult> SearchMerchant( string term, SearchParams searchParams )
        {
            var result = await _customerSearchService.SearchMerchantsAsync(searchParams);
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
