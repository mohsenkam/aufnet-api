using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface IRegionService
    {
        Task<IGetServiceResult<RegionDto>> GetRegionAsync(string name);
        Task<IServiceResult> CreateRegion(RegionDto regionDto);
        Task<IServiceResult> UpdateRegion(string name, RegionDto regionDto);
        Task<IServiceResult> DeleteRegion(string name);
    }
}