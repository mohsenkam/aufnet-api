using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Customer
{
    public interface ICustomerSearcheService
    {
        Task<IGetServiceResult<List<ProductThumbnailDto>>> GetProductsAsync(string order, SearchParams searchParams);
        Task<IGetServiceResult<List<CustomerProductDto>>> GetProductAsync(long id);

        Task<IGetServiceResult<List<MerchantThumbnailDto>>> GetMerchantsAsync(string order, SearchParams searchParams);
        Task<IGetServiceResult<List<CustomerMerchantDto>>> GetMerchantAsync(long id);

        Task<IGetServiceResult<List<LoyaltyBasedOfferThumbnailDto>>> GetLoyaltyBasedOffersAsync(string order,
            SearchParams searchParams);

        Task<IGetServiceResult<List<CustomerLoyaltyBasedOfferDto>>> GetLoyaltyBasedOfferAsync(long id);

        Task<IGetServiceResult<List<QuantityBasedOfferThumbnailDto>>> GetQuantityBasedOffersAsync(string order,
            SearchParams searchParams);

        Task<IGetServiceResult<List<CustomerQuantityBasedOfferDto>>> GetQuantityBasedOfferAsync(long id);

        Task<IGetServiceResult<List<ItemBasedOfferThumbnailDto>>> GetItemBasedOffersAsync(string order,
            SearchParams searchParams);

        Task<IGetServiceResult<List<CustomerItemBasedOfferDto>>> GetItemBasedOfferAsync(long id);

        Task<IGetServiceResult<List<ProductSearchDto>>> SearchProductsAsync(SearchParams searchParams);

        Task<IGetServiceResult<List<MerchanSearchDto>>> SearchMerchantsAsync(SearchParams searchParams);
    }
}