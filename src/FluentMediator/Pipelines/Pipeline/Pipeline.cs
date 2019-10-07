using System;

namespace FluentMediator
{
    public class Pipeline<Request> : IPipeline
    {
        private readonly IMediator _mediator;
        private readonly MethodCollection<Method<Action<object, Request>, Request>, Request > _methods;

        public Pipeline(IMediator mediator)
        {
            _mediator = mediator;
            _methods = new MethodCollection<Method<Action<object, Request>, Request>, Request > ();
        }

        public Pipeline<Request> With<Handler>(Action<Handler, Request> action)
        {
            Action<object, Request> typedHandler = (h, r) => action((Handler) h, (Request) r);
            var method = new Method<Action<object, Request>, Request>(typeof(Handler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public void Publish(object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = _mediator.GetService(handler.HandlerType);
                handler.Action(concreteHandler, (Request) request);
            }
        }
    }
}