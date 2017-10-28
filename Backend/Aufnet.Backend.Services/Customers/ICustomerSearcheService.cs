using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Customers
{
    public interface ICustomerSearcheService
    {
        Task<IGetServiceResult<CustomerMerchantDto>> GetMerchantAsync(long id);

        Task<IGetServiceResult<List<LoyaltyBasedOfferThumbnailDto>>> GetLoyaltyBasedOffersAsync(string order,
            SearchParams searchParams);


        Task<IGetServiceResult<CustomerLoyaltyBasedOfferDto>> GetLoyaltyBasedOfferAsync(long id);

        Task<IGetServiceResult<List<QuantityBasedOfferThumbnailDto>>> GetQuantityBasedOffersAsync(string order,
            SearchParams searchParams);


        Task<IGetServiceResult<CustomerQuantityBasedOfferDto>> GetQuantityBasedOfferAsync(long id);

        Task<IGetServiceResult<List<ItemBasedOfferThumbnailDto>>> GetItemBasedOffersAsync(string order,
            SearchParams searchParams);


        Task<IGetServiceResult<CustomerItemBasedOfferDto>> GetItemBasedOfferAsync(long id);

        Task<IGetServiceResult<List<ProductSearchDto>>> SearchProductsAsync(string order, SearchParams searchParams);
        Task<IGetServiceResult<CustomerProductDto>> GetProductAsync(long id);

        Task<IGetServiceResult<List<MerchanSearchDto>>> SearchMerchantsAsync(SearchParams searchParams);
    }
}