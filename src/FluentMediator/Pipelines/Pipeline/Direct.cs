using System;

namespace FluentMediator.Pipelines.Pipeline
{
    internal sealed class Direct<TRequest, TResult, THandler> : IDirect
    {
        private readonly Method<Func<object, object, TResult>> _method;

        public Direct(Func<THandler, TRequest, TResult> action)
        {
            Func<object, object, TResult> typedHandler = (h, req) => action((THandler) h, (TRequest) req);
            _method = new Method<Func<object, object, TResult>>(typeof(THandler), typedHandler);
        }

        public TResponse Send<TResponse>(GetService getService, object request)
        {
            var concreteHandler = getService(_method.HandlerType);
            return (TResponse) (object) _method.Action((THandler) concreteHandler, (TRequest) request) !;
        }
    }
}