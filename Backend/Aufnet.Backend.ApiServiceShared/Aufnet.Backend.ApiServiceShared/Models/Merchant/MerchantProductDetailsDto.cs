using System;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantProductDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}