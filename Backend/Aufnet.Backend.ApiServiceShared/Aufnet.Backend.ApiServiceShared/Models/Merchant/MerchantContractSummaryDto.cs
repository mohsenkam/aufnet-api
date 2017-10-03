using System;
using System.ComponentModel.DataAnnotations;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantContractSummaryDto
    {
        public long Id { get; set; }
        public string BusinessName { get; set; }

        public string Abn { get; set; }

        public DateTime ContractStartDate { get; set; }

        public string Suburb{ get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

    }
}