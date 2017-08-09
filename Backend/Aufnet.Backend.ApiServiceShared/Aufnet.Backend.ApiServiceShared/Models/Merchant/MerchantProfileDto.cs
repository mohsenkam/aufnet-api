
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantProfileDto
    {
        public string BusinessName { get; set; }
        public virtual AddressDto AddressDto { get; set; }
        public PointDto LocationDto { get; set; }
    }
}