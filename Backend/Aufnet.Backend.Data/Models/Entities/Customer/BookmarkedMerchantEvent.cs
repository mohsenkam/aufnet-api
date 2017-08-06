﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;

namespace Aufnet.Backend.Data.Models.Entities.Customer
{
    public sealed class BookmarkedMerchantEvent: Entity
    {
        public BookmarkedMerchantEvent()
        {
            MerchantEvents = new Collection<MerchantEvent>();
        }
        public ApplicationUser Customer { get; set; }
        public ICollection<MerchantEvent> MerchantEvents { get; set; }
    }
}
