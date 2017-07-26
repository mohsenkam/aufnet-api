using System;
using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models
{
    public class MerchantProfileDto
    {
        public string BusinessName { get; set; }
        public virtual AddressDto AddressDto { get; set; }
    }
}