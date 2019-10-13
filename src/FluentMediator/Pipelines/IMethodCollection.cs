using System.Collections.ObjectModel;

namespace FluentMediator.Pipelines
{
    public interface IMethodCollection<Method, TRequest>
    {
        ReadOnlyCollection<Method> GetMethods();
        void Add(Method method);
    }
}