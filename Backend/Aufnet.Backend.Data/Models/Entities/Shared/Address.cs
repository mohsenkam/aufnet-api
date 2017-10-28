using System.ComponentModel.DataAnnotations;

namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Address:Entity
    {
        [MaxLength(40)]
        [Required]
        public string Country { get; set; }

        [MaxLength(40)]
        [Required]
        public string State { get; set; }

        [MaxLength(40)]
        [Required]
        public string PostCode { get; set; }

        [MaxLength(40)]
        [Required]
        public string Street { get; set; }

        [MaxLength(40)]
        public string Unit { get; set; }

        [MaxLength(210)]
        [Required]
        public string Raw { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
