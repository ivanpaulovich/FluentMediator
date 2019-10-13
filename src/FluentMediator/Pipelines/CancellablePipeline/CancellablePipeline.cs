using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public class CancellablePipeline<TRequest> : ICancellablePipeline, ICancellablePipelineBuilder<TRequest>
    {
        private readonly IMediatorBuilder _mediatorBuilder;
        private readonly IMethodCollection<Method<Func<object, object, CancellationToken, Task>>> _methods;
        private ICancellableAsync _direct;

        public CancellablePipeline(IMediatorBuilder mediatorBuilder)
        {
            _mediatorBuilder = mediatorBuilder;
            _methods = new MethodCollection<Method<Func<object, object, CancellationToken, Task>>> ();
            _direct = null!;
        }

        public ICancellablePipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> func)
        {
            Func<object, object, CancellationToken, Task> typedHandler = async(h, r, c) => await func((THandler) h, (TRequest) r, c);
            var method = new Method<Func<object, object, CancellationToken, Task>>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IMediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> func)
        {
            _direct = new CancellableAsyncDirect<TRequest, TResult, THandler>(func);
            return _mediatorBuilder;
        }

        public async Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken)
        {
            foreach (var handler in _methods.GetMethods())
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

            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (TRequest) request, cancellationToken);
            }

            return await _direct.SendAsync<TResult>(getService, request!, cancellationToken) !;
        }

        public IMediatorBuilder Build()
        {
            return _mediatorBuilder;
        }
    }
}