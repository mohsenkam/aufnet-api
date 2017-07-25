using System.ComponentModel.DataAnnotations;

namespace Aufnet.Backend.ApiServiceShared.Models
{
    public class CustomerChangePasswordDto
    {
        public string Username { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

    }
}
