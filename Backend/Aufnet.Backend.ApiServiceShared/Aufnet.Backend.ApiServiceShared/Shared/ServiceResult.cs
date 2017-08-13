using System.Collections.Generic;


namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class ServiceResult : IServiceResult
    {
        public ServiceResult()
        {
            ErrorMessages = new List<ErrorMessage>();
        }

        public IEnumerable<ErrorMessage> GetErrors()
        {
            return ErrorMessages;
        }

        public void AddError(ErrorMessage error)
        {
            ErrorMessages.Add(error);
        }

        public bool HasError()
        {
            return ErrorMessages.Count != 0;
        }

        private List<ErrorMessage> ErrorMessages { get; }
    }
}