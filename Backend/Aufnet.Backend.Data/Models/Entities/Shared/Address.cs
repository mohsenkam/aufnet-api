using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;
namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        [Range(1, 500)]
        public int Id { get; set; } 
        
        public string Country { get; set; }

        public string State { get; set; }
        public string City { get; set; }
        [MaxLength(80)]
        public string Detail { get; set; }

        public string PostCode { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
