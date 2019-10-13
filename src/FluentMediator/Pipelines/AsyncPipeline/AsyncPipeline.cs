using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public class AsyncPipeline<TRequest> : IAsyncPipeline
    {
        private readonly MediatorBuilder _pipelinesManager;
        private readonly MethodCollection<Method<Func<object, TRequest, Task>, TRequest>, TRequest > _methods;
        private IDirectAsync _direct;

        public AsyncPipeline(MediatorBuilder pipelinesManager)
        {
            _pipelinesManager = pipelinesManager;
            _methods = new MethodCollection<Method<Func<object, TRequest, Task>, TRequest>, TRequest > ();
            _direct = null!;
        }

        public AsyncPipeline<TRequest> Call<THandler>(Func<THandler, TRequest, Task> action)
        {
            Func<object, TRequest, Task> typedHandler = async(h, r) => await action((THandler) h, (TRequest) r);
            var method = new Method<Func<object, TRequest, Task>, TRequest>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public MediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> action)
        {
            var sendPipeline = new DirectAsync<TRequest, TResult, THandler>(action);
            _direct = sendPipeline;
            return _pipelinesManager;
        }

        public async Task PublishAsync(GetService getService, object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (TRequest) request);
            }
        }

        public async Task<TResult> SendAsync<TResult>(GetService getService, object request)
        {
            if (_direct is null)
            {
                throw new ReturnFunctionIsNullException("The return function is null. SendAsync<TResult> method not executed.");
            }

            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (TRequest) request);
            }

            return await _direct.SendAsync<TResult>(getService, request!) !;
        }

        public MediatorBuilder Build()
        {
            return _pipelinesManager;
        }
    }
}