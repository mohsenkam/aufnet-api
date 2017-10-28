using System.Collections.Generic;
using Aufnet.Backend.Data.Models.Entities.Merchants;

namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Category: Entity
    {
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public virtual List<Product> Products { get; set; }
        public long ParentId { get; set; }
    }
}
