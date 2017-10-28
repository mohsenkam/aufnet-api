using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;


namespace Aufnet.Backend.Data.Models.Entities.Reminder
{

    public class Reminder : Entity
    {

        [DataType(DataType.Date)]
        public DateTime TrigerDateTime { get; set; }

        public virtual ItemBasedOffer Event { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
