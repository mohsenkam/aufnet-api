namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class SearchParams
    {
        public string Postcode { get; set; }
        public int? Distance { get; set; }
        public long? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SearchTerm { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }


        public bool IsValid => 
            Offset >= 0 && Count >= 0;
    }
}
