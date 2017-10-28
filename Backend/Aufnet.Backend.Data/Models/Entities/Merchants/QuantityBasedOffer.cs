using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Merchants
{
    public class QuantityBasedOffer : Offer
    {
        /// <summary>
        ///  True: offer applies to the transaction next to the one which meets the criteria, False: offer applies when the criteria is met
        /// </summary>
        [Required]
        public bool AppliesAfterMet { get; set; }
        /// <summary>
        /// Minimum total value spend accross the transactions to become eligible.
        /// </summary>
        [Required]
        public decimal MinTotalValue { get; set; }
        public virtual List<Product> OfferedProducts { get; set; }
        public int DiscountPercentage { get; set; }

        //public virtual List<Transaction> Transactions { get; set; }
    }
}