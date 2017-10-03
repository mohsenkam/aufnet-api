namespace Aufnet.Backend.ApiServiceShared.Models.Admin
{
    public class CategoryDto
    {
        public string DisplayName { get; set; }
        public long ParentId { get; set; }
        public string ImageUrl { get; set; }
    }
}