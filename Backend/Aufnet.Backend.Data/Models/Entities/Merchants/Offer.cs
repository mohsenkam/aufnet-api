using System;
using System.ComponentModel.DataAnnotations;

namespace Aufnet.Backend.Data.Models.Entities.Merchants
{
    public class Offer : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsOver { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}