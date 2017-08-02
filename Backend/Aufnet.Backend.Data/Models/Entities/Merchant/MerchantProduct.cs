using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aufnet.Backend.Data.Models.Entities.Event;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Merchant
{
    public class MerchantProduct :Entity
    {
        [MaxLength(80)]
        public string ProductName { get; set; }
        [MaxLength(80)]
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        public int Discount { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
