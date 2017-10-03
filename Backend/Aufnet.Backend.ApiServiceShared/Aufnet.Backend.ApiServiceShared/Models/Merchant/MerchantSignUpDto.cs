using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantSignUpDto
    {

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string TrackingId { get; set; }

    }
}
