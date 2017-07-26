namespace Aufnet.Backend.ApiServiceShared.Models.Shared
{
    public class ErrorDto
    {


        public ErrorDto(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get;}
        public string Message { get;}
    }
}
