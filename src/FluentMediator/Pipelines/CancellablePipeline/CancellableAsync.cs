using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMediator.Pipelines;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public class CancellableAsync<TRequest, TResult, THandler> : ICancellableAsync
    {
        private readonly Method<Func<THandler, TRequest, CancellationToken, Task<TResult>>> _method;

        public CancellableAsync(Func<THandler, TRequest, CancellationToken, Task<TResult>> action)
        {
            Func<THandler, TRequest, CancellationToken, Task<TResult>> typedHandler = (h, req, ct) => action((THandler) h, req, ct);
            _method = new Method<Func<THandler, TRequest, CancellationToken, Task<TResult>>>(action);
        }

        public async Task<Response1> SendAsync<Response1>(GetService getService, object request, CancellationToken cancellationToken)
        {
            var concreteHandler = getService(typeof(THandler));
            Func<THandler, TRequest, CancellationToken, Task<Response1>> typedHandler = (h, req, ct) => (Task<Response1>) (object) _method.Action((THandler) concreteHandler, (TRequest) (object) request!, ct);
            return await typedHandler((THandler) concreteHandler, (TRequest) (object) request!, cancellationToken);
        }
    }
}