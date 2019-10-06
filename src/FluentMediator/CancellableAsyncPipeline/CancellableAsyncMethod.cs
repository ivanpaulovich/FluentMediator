using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class CancellableAsyncMethod<Request>
    {
        public Type HandlerType { get; }
        public Func<object, Request, CancellationToken, Task> Func { get; }

        public CancellableAsyncMethod(Type handlerType, Func<object, Request, CancellationToken, Task> func)
        {
            HandlerType = handlerType;
            Func = func;
        }
    }
}