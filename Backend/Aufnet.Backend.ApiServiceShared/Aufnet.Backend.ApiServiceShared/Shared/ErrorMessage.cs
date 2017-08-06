namespace Aufnet.Backend.ApiServiceShared.Shared
{ 
    public class ErrorMessage
    {
        private string _code;
        private string _message;

        public ErrorMessage(string code, string message)
        {
            _code = code;
            _message = message;
        }

        public string Code => _code;

        public string Message => _message;
    }
}