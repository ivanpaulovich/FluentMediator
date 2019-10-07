using System;

namespace FluentMediator
{
    public class Pipeline<Request> : IPipeline
    {
        private readonly IMediator _mediator;
        private readonly MethodCollection<Method<Action<object, Request>, Request>, Request > _methods;
        private IDirect _direct;

        public Pipeline(IMediator mediator)
        {
            _mediator = mediator;
            _methods = new MethodCollection<Method<Action<object, Request>, Request>, Request > ();
            _direct = null!;
        }

        public Pipeline<Request> Call<Handler>(Action<Handler, Request> action)
        {
            Action<object, Request> typedHandler = (h, r) => action((Handler) h, (Request) r);
            var method = new Method<Action<object, Request>, Request>(typeof(Handler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IDirect Return<Response, Handler>(Func<Handler, Request, Response> func)
        {
            _direct = new Direct<Request, Response, Handler>(_mediator, func);
            return _direct;
        }

        public void Publish(object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = _mediator.GetService(handler.HandlerType);
                handler.Action(concreteHandler, (Request) request);
            }
        }

        public Response Send<Response>(object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = _mediator.GetService(handler.HandlerType);
                handler.Action(concreteHandler, (Request) request);
            }

            if (_direct is null)
                throw new Exception("Send not configured.");

            return _direct.Send<Response>(request!) !;
        }

        public IMediator Build()
        {
            return _mediator;
        }
    }
}