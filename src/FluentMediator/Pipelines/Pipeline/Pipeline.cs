using System;

namespace FluentMediator.Pipelines.Pipeline
{
    internal sealed class Pipeline : IPipeline
    {
        private readonly IMethodCollection<Method<Action<object, object>>> _methods;
        private readonly IDirect? _direct;

        public Pipeline(IMethodCollection<Method<Action<object, object>>> methods, IDirect? direct, Type requestType, string? name)
        {
            _methods = methods;
            _direct = direct;
            RequestType = requestType;
            Name = name;
        }

        public Type RequestType { get; }
        public string? Name { get; }

        public void Publish(GetService getService, object request)
        {
            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                handler.Action(concreteHandler, request);
            }
        }

        public TResult Send<TResult>(GetService getService, object request)
        {
            if (_direct is null)
            {
                throw new ReturnFunctionIsNullException("The return function is null. Send<TResult> method not executed.");
            }

            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                handler.Action(concreteHandler, request);
            }

            return _direct.Send<TResult>(getService, request!) !;
        }
    }
}