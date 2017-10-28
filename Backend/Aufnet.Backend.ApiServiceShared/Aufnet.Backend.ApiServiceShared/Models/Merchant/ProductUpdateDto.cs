using System;
using Aufnet.Backend.Data.Models.Entities.Shared;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class ProductUpdateDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}