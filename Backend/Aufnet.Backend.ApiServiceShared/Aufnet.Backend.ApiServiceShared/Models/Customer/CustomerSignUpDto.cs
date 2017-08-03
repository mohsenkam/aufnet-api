using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerSignUpDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string Password { get; set; }

    }
}
