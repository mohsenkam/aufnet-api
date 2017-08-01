using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aufnet.Backend.Data.Models.Entities.Event;
using Aufnet.Backend.Data.Models.Entities.Identity;

namespace Aufnet.Backend.Data.Models.Entities.Customer
{
    public sealed class BookmarkedMerchantEvent: Entity
    {
        public BookmarkedMerchantEvent()
        {
            MerchantEvents = new Collection<MerchantEvents>();
        }
        public ApplicationUser Customer { get; set; }
        public ICollection<MerchantEvents> MerchantEvents { get; set; }
    }
}
