using System.Collections.Generic;

namespace Aufnet.Backend.Services.Base
{
    public interface IServiceResult
    {
        IEnumerable<ErrorMessage> GetErrors();
        bool HasError();
        void AddError(ErrorMessage error);
    }
}