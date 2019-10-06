using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class AsyncPipeline<Request> : IAsyncPipeline
    {
        private Mediator _mediator;
        private AsyncMethodCollection<Request> _asyncMethods;

        public AsyncPipeline(Mediator mediator)
        {
            _mediator = mediator;
            _asyncMethods = new AsyncMethodCollection<Request>();          
        }

        public AsyncPipeline<Request> HandlerAsync<Handler>(Func<Handler, Request, Task> action)
        {
            Func<object, Request, Task> typedHandler = async (h, r) => await action((Handler)h, (Request)r);
            var method = new AsyncMethod<Request>(typeof(Handler), typedHandler);
            _asyncMethods.Add(method);
            return this;
        }

        public async Task PublishAsync(object request)
        {
            foreach (var handler in _asyncMethods.GetHandlers())
            {
                var concreteHandler = _mediator.GetConcreteHandler(handler.HandlerType);
                await handler.Func(concreteHandler, (Request)request);
            } 
        }
    }
}