using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class AsyncMethod<Request>
    {
        public Type HandlerType { get; }
        public Func<object, Request, Task> Func { get; }

        public AsyncMethod(Type handlerType, Func<object, Request, Task> func)
        {
            HandlerType = handlerType;
            Func = func;
        }
    }
}