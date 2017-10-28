using System.Collections.Generic;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Merchants
{
    public class Product :Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }
        public virtual long CategoryId { get; set; }

        public virtual Merchant Merchant { get; set; }
        public virtual long MerchantId { get; set; }

        public virtual List<Rating> Ratings { get; set; }
        public virtual ItemBasedOffer ItemBasedOffer { get; set; }

        public virtual List<ProductTransaction> ProductTransactionst { get; set; }

    }

    public class ProductTransaction : Entity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
