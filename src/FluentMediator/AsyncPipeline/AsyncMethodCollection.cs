using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentMediator
{
    public class AsyncMethodCollection<Request>
    {
        private readonly IList<AsyncMethod<Request>> _asyncMethods;

        public AsyncMethodCollection()
        {
            _asyncMethods = new List<AsyncMethod<Request>>();
        }

        public ReadOnlyCollection<AsyncMethod<Request>> GetHandlers()
        {
            return new ReadOnlyCollection<AsyncMethod<Request>>(_asyncMethods);
        }

        public void Add(AsyncMethod<Request> method)
        {
            _asyncMethods.Add(method);
        }
    }
}