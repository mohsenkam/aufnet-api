using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;
namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Address
    {
        [Key]
        public int Id { get; set; }   
        public string Country { get; set; }

        public string State { get; set; }
        public string City { get; set; }

        public int PostCode { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
