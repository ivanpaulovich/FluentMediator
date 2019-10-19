using System.Collections.ObjectModel;

namespace FluentMediator.Pipelines
{
    internal interface IMethodCollection<Method>
    {
        ReadOnlyCollection<Method> GetMethods();
        void Add(Method method);
    }
}