using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Merchant
{
    public class MerchantProfile
    {
        [Key]
        public int Id { get; set; }   
        public string BusinessName { get; set; }
        public Address Address { get; set; }
    

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
