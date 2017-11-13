using System;

namespace Aufnet.Backend.ApiServiceShared.Models.Admin
{
    public class AdminContractSummaryDto
    {
        public string BusinessName { get; set; }

        public string Abn { get; set; }

        public DateTime ContractStartDate { get; set; }

        public string Address { get; set; }

        public string CategoryName { get; set; }
    }
}