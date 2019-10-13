using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMediator.Pipelines;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public class CancellablePipeline<TRequest> : ICancellablePipeline
    {
        private readonly MediatorBuilder _pipelinesManager;
        private readonly MethodCollection<Method<Func<object, TRequest, CancellationToken, Task>, TRequest>, TRequest > _methods;
        private ICancellableAsync _direct;

        public CancellablePipeline(MediatorBuilder pipelinesManager)
        {
            _pipelinesManager = pipelinesManager;
            _methods = new MethodCollection<Method<Func<object, TRequest, CancellationToken, Task>, TRequest>, TRequest > ();
            _direct = null!;
        }

        public CancellablePipeline<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> action)
        {
            Func<object, TRequest, CancellationToken, Task> typedHandler = async(h, r, c) => await action((THandler) h, (TRequest) r, c);
            var method = new Method<Func<object, TRequest, CancellationToken, Task>, TRequest>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public MediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> action)
        {
            var sendPipeline = new CancellableAsync<TRequest, TResult, THandler>(action);
            _direct = sendPipeline;
            return _pipelinesManager;
        }

        public async Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (TRequest) request, cancellationToken);
            }
        }

        public async Task<TResult> SendAsync<TResult>(GetService getService, object request, CancellationToken cancellationToken)
        {
            if (_direct is null)
            {
                throw new ReturnFunctionIsNullException("The return function is null. SendAsync<TResult> method not executed.");
            }

            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (TRequest) request, cancellationToken);
            }

            return await _direct.SendAsync<TResult>(getService, request!, cancellationToken) !;
        }

        public MediatorBuilder Build()
        {
            return _pipelinesManager;
        }
    }
}