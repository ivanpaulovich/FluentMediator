using System;

namespace FluentMediator
{
    public class Direct<Request, Response, Handler> : IDirect
    {
        private readonly Method<Func<Handler, Request, Response>, Request> _method;

        public Direct(Func<Handler, Request, Response> action)
        {
            Func<Handler, Request, Response> typedHandler = (h, req) => action((Handler) h, (Request) req);
            _method = new Method<Func<Handler, Request, Response>, Request>(typeof(Handler), action);
        }

        public object Send(GetService getService, object request)
        {
            var concreteHandler = getService(_method.HandlerType);
            return _method.Action((Handler) concreteHandler, (Request) request) !;
        }

        public Response1 Send<Response1>(GetService getService, object request)
        {
            var concreteHandler = getService(_method.HandlerType);
            return (Response1) (object) _method.Action((Handler) concreteHandler, (Request) request) !;
        }
    }
}