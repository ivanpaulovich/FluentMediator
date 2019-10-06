using System;

namespace FluentMediator
{
    public class Pipeline<Request> : IPipeline
    {
        private Mediator _mediator;
        private MethodCollection<Request> _methods;

        public Pipeline(Mediator mediator)
        {
            _mediator = mediator;
            _methods = new MethodCollection<Request>();          
        }

        public Pipeline<Request> Handler<Handler>(Action<Handler, Request> action)
        {
            Action<object, Request> typedHandler = (h, r) => action((Handler)h, (Request)r);
            var method = new Method<Request>(typeof(Handler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public void Publish(object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = _mediator.GetConcreteHandler(handler.HandlerType);
                handler.Action(concreteHandler, (Request)request);
            } 
        }
    }
}