using System;
using System.Collections.Generic;
using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantCalendarDto
    {
        public List<MerchantEventsDto> EventDtos { get; set; }
    }
}