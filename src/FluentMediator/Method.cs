using System;

namespace FluentMediator
{
    public class Method<Act, Request>
    {
        public Type HandlerType { get; }
        public Act Action { get; }

        public Method(Type handlerType, Act action)
        {
            HandlerType = handlerType;
            Action = action;
        }
    }
}