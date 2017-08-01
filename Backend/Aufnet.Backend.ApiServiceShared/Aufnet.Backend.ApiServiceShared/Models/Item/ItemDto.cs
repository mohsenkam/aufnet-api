using System;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;

namespace Aufnet.Backend.ApiServiceShared.Models.Item
{
    public class ItemDto
    {
        public string   ItemName { get; set; }
        public DateTime EventStarDate { get; set; }
        public DateTime EventEndDate { get; set; }


        public bool IsAvailable { get; set; }
        public int Discount { get; set; }
        
        public virtual MerchantEventsDto MerchantEventsDto { get; set; }
    }
}