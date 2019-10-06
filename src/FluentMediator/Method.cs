using System;

namespace FluentMediator
{
    public class Method<Request>
    {
        public Type HandlerType { get; }
        public Action<object, Request> Action { get; }

        public Method(Type handlerType, Action<object, Request> action)
        {
            HandlerType = handlerType;
            Action = action;
        }
    }
}