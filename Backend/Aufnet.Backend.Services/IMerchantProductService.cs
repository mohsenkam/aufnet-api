using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface IMerchantProductService
    {
        Task<IGetServiceResult<MerchantProductDto>> GetProductAsync(string username);
        Task<IServiceResult> CreateProduct(string username, MerchantProductDto value);
        Task<IServiceResult> UpdateProduct(string username, MerchantProductDto value);
        Task<IServiceResult> DelteProduct(string username);
    }
}