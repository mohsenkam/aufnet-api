namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class GetServiceResult<T> : IGetServiceResult<T>
    {
        public IServiceResult GetResult()
        {
            return this._serviceResult;
        }

        public void SetResult(IServiceResult serviceResult)
        {
            this._serviceResult = serviceResult;
        }

        public T GetData()
        {
            return this._data;
        }

        public void SetData(T data)
        {
            this._data = data;
        }

        public bool HasError()
        {
            return _serviceResult != null && _serviceResult.HasError();
        }

        private T _data;
        private IServiceResult _serviceResult;

    }
}