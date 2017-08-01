using System;
using System.Collections.Generic;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerCalendarDto
    {
        public List<MerchantEventsDto> EventDtos { get; set; }
    }
}