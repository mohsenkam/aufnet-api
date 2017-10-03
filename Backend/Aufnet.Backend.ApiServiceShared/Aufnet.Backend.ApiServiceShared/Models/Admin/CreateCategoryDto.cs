namespace Aufnet.Backend.ApiServiceShared.Models.Admin
{
    public class CreateCategoryDto
    {
        public string DisplayName { get; set; }
        public long? ParentId { get; set; }
    }
}