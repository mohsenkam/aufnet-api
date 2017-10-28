namespace Aufnet.Backend.Data.Models.Entities.Merchants
{
    public class OfferItem: Entity
    {
        public Product Product { get; set; }
        public decimal NewPrice { get; set; }
    }
}