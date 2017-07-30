using System.ComponentModel.DataAnnotations;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

    }
}
