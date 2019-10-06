using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentMediator
{
    public class CancellableAsyncMethodCollection<Request>
    {
        private readonly IList<CancellableAsyncMethod<Request>> _asyncMethods;

        public CancellableAsyncMethodCollection()
        {
            _asyncMethods = new List<CancellableAsyncMethod<Request>>();
        }

        public ReadOnlyCollection<CancellableAsyncMethod<Request>> GetHandlers()
        {
            return new ReadOnlyCollection<CancellableAsyncMethod<Request>>(_asyncMethods);
        }

        public void Add(CancellableAsyncMethod<Request> method)
        {
            _asyncMethods.Add(method);
        }
    }
}