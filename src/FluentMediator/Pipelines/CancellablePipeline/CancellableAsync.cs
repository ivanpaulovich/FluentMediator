using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class CancellableAsync<Request, Response, Handler> : ICancellableAsync
    {
        private readonly Method<Func<Handler, Request, CancellationToken, Task<Response>>> _method;

        public CancellableAsync(Func<Handler, Request, CancellationToken, Task<Response>> action)
        {
            Func<Handler, Request, CancellationToken, Task<Response>> typedHandler = (h, req, ct) => action((Handler) h, req, ct);
            _method = new Method<Func<Handler, Request, CancellationToken, Task<Response>>>(action);
        }

        public async Task<Response1> SendAsync<Response1>(GetService getService, object request, CancellationToken cancellationToken)
        {
            var concreteHandler = getService(typeof(Handler));
            Func<Handler, Request, CancellationToken, Task<Response1>> typedHandler = (h, req, ct) => (Task<Response1>) (object) _method.Action((Handler) concreteHandler, (Request) (object) request!, ct);
            return await typedHandler((Handler) concreteHandler, (Request) (object) request!, cancellationToken);
        }
    }
}