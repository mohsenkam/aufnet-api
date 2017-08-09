using System;
using Aufnet.Backend.Data.Models.Entities.Identity;

namespace Aufnet.Backend.ApiServiceShared.Models.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int PointNumber { get; set; }
        public ApplicationUser Customer { get; set; }
        public ApplicationUser Merchant { get; set; }

    }
}