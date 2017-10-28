using System.Collections.Generic;

namespace Aufnet.Backend.Data.Models.Entities.Merchants
{

    public class ItemBasedOffer : Offer
    {
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
