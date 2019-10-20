using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    internal sealed class Direct<TRequest, TResult, THandler> : IDirect
    {
        private readonly Method<Func<object, object, CancellationToken, Task<TResult>>> _method;

        public Direct(Func<THandler, TRequest, CancellationToken, Task<TResult>> action)
        {
            Func<object, object, CancellationToken, Task<TResult>> typedHandler = (h, req, ct) => action((THandler) h, (TRequest) req, ct);
            _method = new Method<Func<object, object, CancellationToken, Task<TResult>>>(typeof(THandler), typedHandler);
        }

        public async Task<TResponse> SendAsync<TResponse>(GetService getService, object request, CancellationToken cancellationToken)
        {
            var concreteHandler = getService(typeof(THandler));
            Func<THandler, TRequest, CancellationToken, Task<TResponse>> typedHandler = (h, req, ct) => (Task<TResponse>) (object) _method.Action((THandler) concreteHandler, (TRequest) (object) request!, ct);
            return await typedHandler((THandler) concreteHandler, (TRequest) (object) request!, cancellationToken);
        }
    }
}