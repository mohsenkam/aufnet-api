using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Customers;
using Aufnet.Backend.Data.Models.Entities.Merchants;

namespace Aufnet.Backend.Data.Models.Entities.Shared
{

    public class Transaction : Entity
    {
        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }

        public enum TransactionStatus
        {
            ReadyToPay,
            Paid,
            Acepted,
            Rejected
        }

        public TransactionStatus Status { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<ProductTransaction> ProductTransactionst { get; set; }
        //public virtual LoyaltyBasedOffer LoyaltyBasedOfferUsed { get; set; }
        //public virtual QuantityBasedOffer QuantityBasedOfferUsed { get; set; }
        //public virtual Shopping Shopping { get; set; }
    }


    public class Shopping : Entity
    {
        //public DateTime DateTime { get; set; }
        //public Customer Customer { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public ShoppingPayment ShoppingPayment { get; set; } // Customer to Aufnet
    }

    public class ShoppingPayment : Entity
    {
        public enum PaymentStatus
        {
            Started,
            Authorized,
            Reversal
        }
        
    }
}
