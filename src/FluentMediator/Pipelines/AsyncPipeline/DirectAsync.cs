using System;
using System.Threading.Tasks;
using FluentMediator.Pipelines;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public class DirectAsync<TRequest, TResult, THandler> : IDirectAsync
    {
        private readonly Method<Func<object, object, Task<TResult>>> _method;

        public DirectAsync(Func<THandler, TRequest, Task<TResult>> action)
        {
            Func<object, object, Task<TResult>> typedHandler = (h, req) => action((THandler) h, (TRequest)req);
            _method = new Method<Func<object, object, Task<TResult>>>(typeof(THandler), typedHandler);
        }

        public async Task<Response1> SendAsync<Response1>(GetService getService, object request)
        {
            var concreteHandler = getService(typeof(THandler));
            Func<THandler, TRequest, Task<Response1>> typedHandler = (h, req) => (Task<Response1>) (object) _method.Action((THandler) concreteHandler, (TRequest) (object) request!);
            return await typedHandler((THandler) concreteHandler, (TRequest) (object) request!);
        }
    }
}