using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aufnet.Backend.Api.Models
{
    public class UserDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string Password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Roles { get; set; }

        [Required]
        public string Username { get; set; }        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
