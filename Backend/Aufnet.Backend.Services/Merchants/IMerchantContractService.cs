using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Microsoft.AspNetCore.Http;

namespace Aufnet.Backend.Services.Merchants
{
    public interface IMerchantContractService
    {
        Task<IServiceResult> UpateContractDetailsAsync( MerchantCreateDto value ); // CREATES THE MERCHANT and the contract
        Task<IServiceResult> UpdateLogoAsync( long id, IFormFile file);
    }
}