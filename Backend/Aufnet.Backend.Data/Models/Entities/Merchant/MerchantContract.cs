using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Merchant
{
    public class MerchantContract : Entity
    {
        [Required]
        public string BusinessName { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OwnerName { get; set; }

        [Required]
        public string Abn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ContractStartDate { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string SubCategory { get; set; }

        public string LogoUri { get; set; }

        public string TrackingId { get; set; }

        public bool IsTrackingIdUsed { get; set; } = false;

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}