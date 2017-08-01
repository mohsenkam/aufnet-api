using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Aufnet.Backend.Data.Models.Entities.Event;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Remotion.Linq.Clauses;

namespace Aufnet.Backend.Data.Models.Entities.Item
{

    public class Item: Entity
    {

        public string ItemName { get; set; }
        
        public bool IsAvailable { get; set; }
        public int Discount { get; set; }
        [Column(Order = 2)]
        public virtual MerchantEvents Events { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
