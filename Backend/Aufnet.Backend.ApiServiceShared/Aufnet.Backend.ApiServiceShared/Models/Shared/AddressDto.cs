namespace Aufnet.Backend.ApiServiceShared.Models.Shared
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Detail { get; set; }
        public string PostCode { get; set; }
    }
}
