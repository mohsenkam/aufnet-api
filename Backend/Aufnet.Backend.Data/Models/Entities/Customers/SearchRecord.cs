namespace Aufnet.Backend.Data.Models.Entities.Customers
{
    public class SearchRecord: Entity
    {
        public string Term { get; set; }
        public string Address { get; set; }
        public SearchType SearchType { get; set; }
    }

    public enum SearchType
    {
        Product,
        Merchant
    }
}