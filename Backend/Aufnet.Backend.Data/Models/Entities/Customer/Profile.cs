using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Aufnet.Backend.Data.Models.Entities.Identity;

namespace Aufnet.Backend.Data.Models.Entities.Customer
{
    public class Profile
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        private int Test;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
        //todo: Add photo
        //todo: Add barcode

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
