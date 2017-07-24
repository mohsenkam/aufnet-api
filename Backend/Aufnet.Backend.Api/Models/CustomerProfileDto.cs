using System;

namespace Aufnet.Backend.Api.Models
{
    public class CustomerProfileDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
    }
}