using System.Collections.Generic;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Customers
{
    public class Customer: Entity
    {
        public virtual ApplicationUser User { get; set; }
        public virtual CustomerProfile Profile { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<SearchRecord> SearchHistory { get; set; }
    }
}
