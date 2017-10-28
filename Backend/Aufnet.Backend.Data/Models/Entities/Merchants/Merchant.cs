using System.Collections.Generic;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Merchants
{
    public class Merchant: Entity
    {
        public virtual ApplicationUser User { get; set; }
        public virtual List<OperationStatus> OperationStatus { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual Contract Contract { get; set; }
        //public virtual List<Transaction> Transactions { get; set; }
        public virtual List<QuantityBasedOffer> QuantityBasedOffers { get; set; }
        public virtual List<LoyaltyBasedOffer> LoyaltyBasedOffers { get; set; }
    }
}
