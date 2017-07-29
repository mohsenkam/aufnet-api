using System;
using System.Collections.Generic;
using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerCalendarDto
    {
        public List<EventDto> EventDtos { get; set; }
    }

    public class EventDto
    {
        public string Title { get; set; }
        public string MerchantUserName { get; set; }
        public DateTime EventDate { get; set; }
    }
}