using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;

namespace Aufnet.Backend.Data.Models.Entities.Event
{

    public class MerchantEvents : Entity
    {

        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StarDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        //
        public virtual MerchantProduct MerchantProduct { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
