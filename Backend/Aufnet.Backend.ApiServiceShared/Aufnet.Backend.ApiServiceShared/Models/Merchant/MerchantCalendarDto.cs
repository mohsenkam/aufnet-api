using System;
using System.Collections.Generic;
using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantCalendarDto
    {
        public List<MerchantEventDto> EventDtos { get; set; }
    }

    public class MerchantEventDto
    {
        public string Title { get; set; }
        public string MerchantUserName { get; set; }
        public DateTime EventDate { get; set; }
    }
}