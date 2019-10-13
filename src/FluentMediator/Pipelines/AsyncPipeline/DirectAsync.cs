using System;
using System.Threading.Tasks;
using FluentMediator.Pipelines;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public class DirectAsync<TRequest, TResult, THandler> : IDirectAsync
    {
        private readonly Method<Func<THandler, TRequest, Task<TResult>>> _method;

        public DirectAsync(Func<THandler, TRequest, Task<TResult>> action)
        {
            Func<THandler, TRequest, Task<TResult>> typedHandler = (h, req) => action((THandler) h, req);
            _method = new Method<Func<THandler, TRequest, Task<TResult>>>(action);
        }

        public async Task<Response1> SendAsync<Response1>(GetService getService, object request)
        {
            var concreteHandler = getService(typeof(THandler));
            Func<THandler, TRequest, Task<Response1>> typedHandler = (h, req) => (Task<Response1>) (object) _method.Action((THandler) concreteHandler, (TRequest) (object) request!);
            return await typedHandler((THandler) concreteHandler, (TRequest) (object) request!);
        }
    }
}