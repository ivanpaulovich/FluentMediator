using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class CancellablePipeline<Request> : ICancellablePipeline
    {
        private readonly PipelinesManager _pipelinesManager;
        private readonly MethodCollection<Method<Func<object, Request, CancellationToken, Task>, Request>, Request > _methods;
        private ICancellableAsync _direct;

        public CancellablePipeline(PipelinesManager pipelinesManager)
        {
            _pipelinesManager = pipelinesManager;
            _methods = new MethodCollection<Method<Func<object, Request, CancellationToken, Task>, Request>, Request > ();
            _direct = null!;
        }

        public CancellablePipeline<Request> Call<Handler>(Func<Handler, Request, CancellationToken, Task> action)
        {
            Func<object, Request, CancellationToken, Task> typedHandler = async(h, r, c) => await action((Handler) h, (Request) r, c);
            var method = new Method<Func<object, Request, CancellationToken, Task>, Request>(typeof(Handler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public PipelinesManager Return<Response, Handler>(Func<Handler, Request, CancellationToken, Task<Response>> action)
        {
            var sendPipeline = new CancellableAsync<Request, Response, Handler>(action);
            _direct = sendPipeline;
            return _pipelinesManager;
        }

        public async Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (Request) request, cancellationToken);
            }
        }

        public async Task<Response> SendAsync<Response>(GetService getService, object request, CancellationToken cancellationToken)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (Request) request, cancellationToken);
            }

            return await _direct.SendAsync<Response>(getService, request!, cancellationToken) !;
        }

        public PipelinesManager Build()
        {
            return _pipelinesManager;
        }
    }
}