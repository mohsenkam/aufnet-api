using System;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models
{
    public class MerchantProfileDto
    {
        public string BusinessName { get; set; }
        public virtual Address Address { get; set; }
    }
}