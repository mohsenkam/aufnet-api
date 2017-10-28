using System.Collections.Generic;

namespace Aufnet.Backend.ApiServiceShared.Models.Customer
{
    public class SaveTransactionDto
    {
        public List<long> Products { get; set; }
    }
}