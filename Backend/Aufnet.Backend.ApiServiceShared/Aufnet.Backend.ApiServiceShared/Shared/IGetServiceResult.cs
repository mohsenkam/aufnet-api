using System.Collections.Generic;


namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public interface IGetServiceResult<T>
    {
        IServiceResult GetResult();
        void SetResult(IServiceResult serviceResult);
        T GetData();
        void SetData(T data);
        bool HasError();
        int GetTotalCount();
        void SetTotalCount(int totalCount);
    }
}