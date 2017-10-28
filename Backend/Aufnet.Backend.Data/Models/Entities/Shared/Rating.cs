using Aufnet.Backend.Data.Models.Entities.Merchants;

namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Rating: Entity
    {
        public int Rate { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customers.Customer Customer { get; set; }
    }
}