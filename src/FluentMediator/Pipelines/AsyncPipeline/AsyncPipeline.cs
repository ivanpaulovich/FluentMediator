using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class AsyncPipeline<Request> : IAsyncPipeline
    {
        private readonly IMediator _mediator;
        private readonly MethodCollection<Method<Func<object, Request, Task>, Request>, Request > _methods;
        private IDirectAsync _direct;

        public AsyncPipeline(IMediator mediator)
        {
            _mediator = mediator;
            _methods = new MethodCollection<Method<Func<object, Request, Task>, Request>, Request > ();
            _direct = null!;
        }

        public AsyncPipeline<Request> Call<Handler>(Func<Handler, Request, Task> action)
        {
            Func<object, Request, Task> typedHandler = async(h, r) => await action((Handler) h, (Request) r);
            var method = new Method<Func<object, Request, Task>, Request>(typeof(Handler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IMediator Return<Response, Handler>(Func<Handler, Request, Task<Response>> action)
        {
            var sendPipeline = new DirectAsync<Request, Response, Handler>(_mediator, action);
            _direct = sendPipeline;
            return _mediator;
        }

        public async Task PublishAsync(object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = _mediator.GetService(handler.HandlerType);
                await handler.Action(concreteHandler, (Request) request);
            }
        }

        public async Task<Response> SendAsync<Response>(object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = _mediator.GetService(handler.HandlerType);
                await handler.Action(concreteHandler, (Request) request);
            }

            return await _direct.SendAsync<Response>(request!) !;
        }

        public IMediator Build()
        {
            return _mediator;
        }
    }
}