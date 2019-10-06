using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class CancellableAsyncPipeline<Request> : ICancellableAsyncPipeline
    {
        private Mediator _mediator;
        private CancellableAsyncMethodCollection<Request> _asyncMethods;

        public CancellableAsyncPipeline(Mediator mediator)
        {
            _mediator = mediator;
            _asyncMethods = new CancellableAsyncMethodCollection<Request>();          
        }

        public CancellableAsyncPipeline<Request> HandlerAsync<Handler>(Func<Handler, Request, CancellationToken, Task> action)
        {
            Func<object, Request, CancellationToken, Task> typedHandler = async (h, r, c) => await action((Handler)h, (Request)r, c);
            var method = new CancellableAsyncMethod<Request>(typeof(Handler), typedHandler);
            _asyncMethods.Add(method);
            return this;
        }

        public async Task PublishAsync(object request, CancellationToken cancellationToken)
        {
            foreach (var handler in _asyncMethods.GetHandlers())
            {
                var concreteHandler = _mediator.GetConcreteHandler(handler.HandlerType);
                await handler.Func(concreteHandler, (Request)request, cancellationToken);
            } 
        }
    }
}