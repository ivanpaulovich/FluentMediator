using System;

namespace FluentMediator
{
    public class Direct<Request, Response, Handler> : IDirect
    {
        private readonly IMediator _mediator;

        private readonly Method<Func<Handler, Request, Response>, Request> _method;

        public Direct(IMediator mediator, Func<Handler, Request, Response> action)
        {
            _mediator = mediator;
            Func<Handler, Request, Response> typedHandler = (h, req) => action((Handler) h, (Request) req);
            _method = new Method<Func<Handler, Request, Response>, Request>(typeof(Handler), action);
        }

        public object Send(object request)
        {
            var concreteHandler = _mediator.GetService(_method.HandlerType);
            return _method.Action((Handler) concreteHandler, (Request) request) !;
        }

        public Response1 Send<Response1>(object request)
        {
            var concreteHandler = _mediator.GetService(_method.HandlerType);
            return (Response1) (object) _method.Action((Handler) concreteHandler, (Request) request) !;
        }
    }
}