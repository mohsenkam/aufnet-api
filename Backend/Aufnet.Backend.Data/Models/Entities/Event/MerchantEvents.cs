using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Data.Models.Entities.Shared;

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


        public virtual ApplicationUser Merchant { get; set; }

    }
}
