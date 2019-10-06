using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class SendAsyncPipeline<Request, Response, Handler> : ISendAsyncPipeline
    {
        private readonly Mediator _mediator;
        
        private readonly Method<Func<Handler, Request, Task<Response>>> _method;

        public SendAsyncPipeline(Mediator mediator, Func<Handler, Request, Task<Response>> action)
        {
            _mediator = mediator;
            Func<Handler, Request, Task<Response>> typedHandler = (h, req) => action((Handler) h, req);
            _method = new Method<Func<Handler, Request, Task<Response>>>(action);
        }

        public async Task<Response1> SendAsync<Request1, Response1>(Request1 request)
        {
            var concreteHandler = _mediator.GetService(typeof(Handler));
            Func<Handler, Request, Task<Response1>> typedHandler = (h, req) => (Task<Response1>)(object)_method.Action((Handler)concreteHandler, (Request)(object)request!);
            return await typedHandler((Handler)concreteHandler, (Request)(object)request!);
        }
    }
}