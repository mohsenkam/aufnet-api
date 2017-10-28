using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Merchants
{
    public class Contract : Entity
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
        public Category Category { get; set; }

        //[Required]
        //public string SubCategory { get; set; }

        public string LogoUri { get; set; }

        //public string TrackingId { get; set; }

        //public bool IsTrackingIdUsed { get; set; } = false;
    }
}