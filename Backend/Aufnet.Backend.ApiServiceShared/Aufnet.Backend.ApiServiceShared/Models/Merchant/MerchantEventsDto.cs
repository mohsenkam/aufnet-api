
using System;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantEventsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MerchantUserName { get; set; }

        public virtual MerchantProductDto MerchantProductDto { get; set; }
    }
}