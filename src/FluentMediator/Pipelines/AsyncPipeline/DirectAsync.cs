using System;
using System.Threading.Tasks;
using FluentMediator.Pipelines;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public class DirectAsync<Request, Response, Handler> : IDirectAsync
    {
        private readonly Method<Func<Handler, Request, Task<Response>>> _method;

        public DirectAsync(Func<Handler, Request, Task<Response>> action)
        {
            Func<Handler, Request, Task<Response>> typedHandler = (h, req) => action((Handler) h, req);
            _method = new Method<Func<Handler, Request, Task<Response>>>(action);
        }

        public async Task<Response1> SendAsync<Response1>(GetService getService, object request)
        {
            var concreteHandler = getService(typeof(Handler));
            Func<Handler, Request, Task<Response1>> typedHandler = (h, req) => (Task<Response1>) (object) _method.Action((Handler) concreteHandler, (Request) (object) request!);
            return await typedHandler((Handler) concreteHandler, (Request) (object) request!);
        }
    }
}