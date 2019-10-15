using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public sealed class DirectAsync<TRequest, TResult, THandler> : IDirectAsync
    {
        private readonly Method<Func<object, object, Task<TResult>>> _method;

        public DirectAsync(Func<THandler, TRequest, Task<TResult>> action)
        {
            Func<object, object, Task<TResult>> typedHandler = (h, req) => action((THandler) h, (TRequest) req);
            _method = new Method<Func<object, object, Task<TResult>>>(typeof(THandler), typedHandler);
        }

        public async Task<TResponse> SendAsync<TResponse>(GetService getService, object request)
        {
            var concreteHandler = getService(typeof(THandler));
            Func<THandler, TRequest, Task<TResponse>> typedHandler = (h, req) => (Task<TResponse>) (object) _method.Action((THandler) concreteHandler, (TRequest) (object) request!);
            return await typedHandler((THandler) concreteHandler, (TRequest) (object) request!);
        }
    }
}