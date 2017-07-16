using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Aufnet.Backend.Data.Models.Entities.Identity
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(80)]
        public string FirstName { get; set; }
        [MaxLength(80)]
        public string LastName { get; set; }
    }
}
