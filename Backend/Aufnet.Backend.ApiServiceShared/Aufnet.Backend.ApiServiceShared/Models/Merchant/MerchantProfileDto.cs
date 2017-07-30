using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantProfileDto
    {
        public string BusinessName { get; set; }
        public virtual AddressDto AddressDto { get; set; }
    }
}