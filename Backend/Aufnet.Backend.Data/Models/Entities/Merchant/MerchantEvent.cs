using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Identity;

namespace Aufnet.Backend.Data.Models.Entities.Merchant
{

    public class MerchantEvent : Entity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StarDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        //
        public IList<MerchantProduct> MerchantProducts { set; get; }
        //public virtual MerchantProduct MerchantProduct { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
