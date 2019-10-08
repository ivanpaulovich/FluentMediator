using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class AsyncPipeline<Request> : IAsyncPipeline
    {
        private readonly PipelinesManager _pipelinesManager;
        private readonly MethodCollection<Method<Func<object, Request, Task>, Request>, Request > _methods;
        private IDirectAsync _direct;

        public AsyncPipeline(PipelinesManager pipelinesManager)
        {
            _pipelinesManager = pipelinesManager;
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

        public PipelinesManager Return<Response, Handler>(Func<Handler, Request, Task<Response>> action)
        {
            var sendPipeline = new DirectAsync<Request, Response, Handler>(action);
            _direct = sendPipeline;
            return _pipelinesManager;
        }

        public async Task PublishAsync(GetService getService, object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (Request) request);
            }
        }

        public async Task<Response> SendAsync<Response>(GetService getService, object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (Request) request);
            }

            return await _direct.SendAsync<Response>(getService, request!) !;
        }

        public PipelinesManager Build()
        {
            return _pipelinesManager;
        }
    }
}