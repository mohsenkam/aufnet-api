using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Data.Models.Entities.Shared;
namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Address:Entity
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Detail { get; set; }
        public string PostCode { get; set; }

    }
}
