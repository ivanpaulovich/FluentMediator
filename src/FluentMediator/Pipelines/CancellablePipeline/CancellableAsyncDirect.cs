using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public class CancellableAsyncDirect<TRequest, TResult, THandler> : ICancellableAsync
    {
        private readonly Method<Func<object, object, CancellationToken, Task<TResult>>> _method;

        public CancellableAsyncDirect(Func<THandler, TRequest, CancellationToken, Task<TResult>> action)
        {
            Func<object, object, CancellationToken, Task<TResult>> typedHandler = (h, req, ct) => action((THandler) h, (TRequest)req, ct);
            _method = new Method<Func<object, object, CancellationToken, Task<TResult>>>(typeof(THandler), typedHandler);
        }

        public async Task<Response1> SendAsync<Response1>(GetService getService, object request, CancellationToken cancellationToken)
        {
            var concreteHandler = getService(typeof(THandler));
            Func<THandler, TRequest, CancellationToken, Task<Response1>> typedHandler = (h, req, ct) => (Task<Response1>) (object) _method.Action((THandler) concreteHandler, (TRequest) (object) request!, ct);
            return await typedHandler((THandler) concreteHandler, (TRequest) (object) request!, cancellationToken);
        }
    }
}