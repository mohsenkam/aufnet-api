using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.Data.Models.Entities.Customers
{

    public class CustomerProfile : Entity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customers.Customer Customer { get; set; }
        public virtual long CustomerId { get; set; }

    }
}
