using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Microsoft.AspNetCore.Http;

namespace Aufnet.Backend.Services.Merchants
{
    public interface IMerchantProductService
    {
        Task<IGetServiceResult<List<MerchantProductSummaryDto>>> GetProductsAsync(string username);
        Task<IGetServiceResult<MerchantProductDetailsDto>> GetProductAsync( string username, long id);
        Task<IServiceResult> CreateProduct(string username, CreateProductDto value);
        Task<IServiceResult> UpdateProductDetails(string username, long productId, ProductUpdateDto value);
        Task<IServiceResult> AddOrUpdateImageAsync( long id, IFormFile file );
        Task<IServiceResult> DeleteProduct(string username, long productId);
    }
}