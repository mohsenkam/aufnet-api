using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aufnet.Backend.Api.Models
{
    public class UserDto
    {
        
        [Required]
        public string Username { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

    }
}
