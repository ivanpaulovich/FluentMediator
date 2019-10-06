using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentMediator
{
    public class MethodCollection<Request>
    {
        private readonly IList<Method<Request>> _methods;

        public MethodCollection()
        {
            _methods = new List<Method<Request>>();
        }

        public ReadOnlyCollection<Method<Request>> GetHandlers()
        {
            return new ReadOnlyCollection<Method<Request>>(_methods);
        }

        public void Add(Method<Request> method)
        {
            _methods.Add(method);
        }
    }
}