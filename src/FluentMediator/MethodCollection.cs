using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentMediator
{
    public class MethodCollection<Method, Request>
    {
        private readonly IList<Method> _asyncMethods;

        public MethodCollection()
        {
            _asyncMethods = new List<Method>();
        }

        public ReadOnlyCollection<Method> GetHandlers()
        {
            return new ReadOnlyCollection<Method>(_asyncMethods);
        }

        public void Add(Method method)
        {
            _asyncMethods.Add(method);
        }
    }
}