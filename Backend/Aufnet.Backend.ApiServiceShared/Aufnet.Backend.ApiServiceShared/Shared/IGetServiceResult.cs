using System.Collections.Generic;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public interface IGetServiceResult<T>
    {
        IServiceResult GetResult();
        void SetResult(IServiceResult serviceResult);
        T GetData();
        void SetData(T data);
        bool HasError();
    }
}