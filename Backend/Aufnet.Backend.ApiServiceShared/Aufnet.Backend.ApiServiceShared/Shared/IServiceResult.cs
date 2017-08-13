using System.Collections.Generic;


namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public interface IServiceResult
    {
        IEnumerable<ErrorMessage> GetErrors();
        bool HasError();
        void AddError(ErrorMessage error);
    }
}