using System.Collections.Generic;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public interface IServiceResult
    {
        IEnumerable<ErrorMessage> GetErrors();
        bool HasError();
        void AddError(ErrorMessage error);
    }
}