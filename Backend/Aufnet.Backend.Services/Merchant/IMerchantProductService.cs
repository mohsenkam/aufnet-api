using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services.Merchant
{
    public interface IMerchantProductService
    {
        Task<IGetServiceResult<List<MerchantProductDto>>> GetProductsAsync(string username);
        Task<IGetServiceResult<MerchantProductDto>> GetProductAsync( string username, long id);
        Task<IServiceResult> CreateProduct(string username, MerchantProductDto value);
        Task<IServiceResult> UpdateProduct(string username, MerchantProductDto value);
        Task<IServiceResult> DelteProduct(string username, long productId);
    }
}