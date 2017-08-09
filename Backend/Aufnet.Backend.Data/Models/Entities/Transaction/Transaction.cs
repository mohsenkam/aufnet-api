using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Identity;


namespace Aufnet.Backend.Data.Models.Entities.Transaction
{

    public class Transaction : Entity
    {
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public int PointNumber { get; set; }

        public virtual ApplicationUser Customer { get; set; }
        public virtual ApplicationUser Merchant { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

       
    }
}
