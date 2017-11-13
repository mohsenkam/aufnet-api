using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantCreateDto
    {
        [Required]
        public string BusinessName { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string OwnerName { get; set; }

        [Required]
        public string Abn { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime ContractStartDate { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public long CategoryId { get; set; }

    }
}