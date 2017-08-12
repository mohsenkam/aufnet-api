using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;


namespace Aufnet.Backend.Data.Models.Entities.Reminder
{

    public class Reminder : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime TrigerDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime TrigerTime { get; set; }

        public  virtual MerchantEvent Event { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
