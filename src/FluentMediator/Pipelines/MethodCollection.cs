using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentMediator.Pipelines
{
    public sealed class MethodCollection<Method> : IMethodCollection<Method>
    {
        private readonly IList<Method> _asyncMethods;

        public MethodCollection()
        {
            _asyncMethods = new List<Method>();
        }

        public ReadOnlyCollection<Method> GetMethods()
        {
            return new ReadOnlyCollection<Method>(_asyncMethods);
        }

        public void Add(Method method)
        {
            _asyncMethods.Add(method);
        }
    }
}