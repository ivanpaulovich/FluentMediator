using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class SendAsyncPipeline<Request, Response, Handler> : ISendAsyncPipeline
    {
        private readonly Mediator _mediator;
        
        private readonly Method<Func<Handler, Request, Task<Response>>, Request> _method;

        public SendAsyncPipeline(Mediator mediator, Func<Handler, Request, Task<Response>> action)
        {
            _mediator = mediator;
            Func<Handler, Request, Task<Response>> typedHandler = (h, req) => action((Handler) h, (Request) req);
            _method = new Method<Func<Handler, Request, Task<Response>>, Request>(typeof(Handler), action);
        }

        public async Task<Response1> SendAsync<Request1, Response1>(Request1 request)
        {
            var concreteHandler = _mediator.GetService(_method.HandlerType);
            return await _method.Action((Handler)concreteHandler, (dynamic) request!)!;
        }
    }
}