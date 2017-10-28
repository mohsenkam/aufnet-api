namespace Aufnet.Backend.ApiServiceShared.Models.Admin
{
    public class CategoryDto
    {
        public string DisplayName { get; set; }
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public long?[] SubCategories { get; set; }
    }
}