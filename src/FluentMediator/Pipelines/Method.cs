using System;

namespace FluentMediator.Pipelines
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

    public class Method<Act>
    {
        public Act Action { get; }

        public Method(Act action)
        {
            Action = action;
        }
    }
}