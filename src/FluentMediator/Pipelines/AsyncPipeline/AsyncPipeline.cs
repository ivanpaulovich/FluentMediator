using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public class AsyncPipeline<TRequest> : IAsyncPipeline, IAsyncPipelineBuilder<TRequest>
    {
        private readonly IMediatorBuilder _mediatorBuilder;
        private readonly IMethodCollection<Method<Func<object, object, Task>>> _methods;
        private IDirectAsync _direct;

        public AsyncPipeline(IMediatorBuilder mediatorBuilder)
        {
            _mediatorBuilder = mediatorBuilder;
            _methods = new MethodCollection<Method<Func<object, object, Task>>> ();
            _direct = null!;
        }

        public IAsyncPipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, Task> func)
        {
            Func<object, object, Task> typedHandler = async(h, r) => await func((THandler) h, (TRequest) r);
            var method = new Method<Func<object, object, Task>>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IMediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> func)
        {
            _direct = new DirectAsync<TRequest, TResult, THandler>(func);
            return _mediatorBuilder;
        }

        public async Task PublishAsync(GetService getService, object request)
        {
            foreach (var handler in _methods.GetMethods())
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

            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, (TRequest) request);
            }

            return await _direct.SendAsync<TResult>(getService, request!) !;
        }

        public IMediatorBuilder Build()
        {
            return _mediatorBuilder;
        }
    }
}