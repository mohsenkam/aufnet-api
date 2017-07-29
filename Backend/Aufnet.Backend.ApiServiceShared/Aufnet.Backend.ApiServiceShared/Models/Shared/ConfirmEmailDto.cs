namespace Aufnet.Backend.ApiServiceShared.Models.Shared
{
    public class ConfirmEmailDto
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}