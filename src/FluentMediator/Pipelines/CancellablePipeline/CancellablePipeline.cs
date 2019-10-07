using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class CancellablePipeline<Request> : ICancellablePipeline
    {
        private readonly Mediator _mediator;
        private readonly MethodCollection<Method<Func<object, Request, CancellationToken, Task>, Request>, Request > _methods;

        public CancellablePipeline(Mediator mediator)
        {
            _mediator = mediator;
            _methods = new MethodCollection<Method<Func<object, Request, CancellationToken, Task>, Request>, Request > ();
        }

        public CancellablePipeline<Request> HandlerAsync<Handler>(Func<Handler, Request, CancellationToken, Task> action)
        {
            Func<object, Request, CancellationToken, Task> typedHandler = async(h, r, c) => await action((Handler) h, (Request) r, c);
            var method = new Method<Func<object, Request, CancellationToken, Task>, Request>(typeof(Handler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public async Task PublishAsync(object request, CancellationToken cancellationToken)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = _mediator.GetService(handler.HandlerType);
                await handler.Action(concreteHandler, (Request) request, cancellationToken);
            }
        }
    }
}