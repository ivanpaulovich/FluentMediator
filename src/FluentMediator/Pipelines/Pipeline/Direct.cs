using System;

namespace FluentMediator.Pipelines.Pipeline
{
    public sealed class Direct<TRequest, TResult, THandler> : IDirect
    {
        private readonly Method<Func<THandler, TRequest, TResult>, TRequest> _method;

        public Direct(Func<THandler, TRequest, TResult> action)
        {
            Func<THandler, TRequest, TResult> typedHandler = (h, req) => action((THandler) h, (TRequest) req);
            _method = new Method<Func<THandler, TRequest, TResult>, TRequest>(typeof(THandler), action);
        }

        public Response1 Send<Response1>(GetService getService, object request)
        {
            var concreteHandler = getService(_method.HandlerType);
            return (Response1) (object) _method.Action((THandler) concreteHandler, (TRequest) request) !;
        }
    }
}