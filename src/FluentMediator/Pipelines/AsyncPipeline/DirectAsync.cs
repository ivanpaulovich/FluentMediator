using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class DirectAsync<Request, Response, Handler> : IDirectAsync
    {
        private readonly IMediator _mediator;

        private readonly Method<Func<Handler, Request, Task<Response>>> _method;

        public DirectAsync(IMediator mediator, Func<Handler, Request, Task<Response>> action)
        {
            _mediator = mediator;
            Func<Handler, Request, Task<Response>> typedHandler = (h, req) => action((Handler) h, req);
            _method = new Method<Func<Handler, Request, Task<Response>>>(action);
        }

        public async Task<Response1> SendAsync<Response1>(object request)
        {
            var concreteHandler = _mediator.GetService(typeof(Handler));
            Func<Handler, Request, Task<Response1>> typedHandler = (h, req) => (Task<Response1>) (object) _method.Action((Handler) concreteHandler, (Request) (object) request!);
            return await typedHandler((Handler) concreteHandler, (Request) (object) request!);
        }
    }
}