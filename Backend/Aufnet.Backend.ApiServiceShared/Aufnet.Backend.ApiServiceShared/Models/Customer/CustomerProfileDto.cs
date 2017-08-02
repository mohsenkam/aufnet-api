using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class CustomerProfileDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
    }
}