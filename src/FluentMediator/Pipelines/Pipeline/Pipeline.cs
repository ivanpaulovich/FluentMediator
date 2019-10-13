using System;

namespace FluentMediator.Pipelines.Pipeline
{
    public class Pipeline<TRequest> : IPipeline, IPipelineBuilder<TRequest>
    {
        private readonly IMediatorBuilder _mediatorBuilder;
        private readonly IMethodCollection<Method<Action<object, object>>> _methods;
        private IDirect _direct;

        public Pipeline(IMediatorBuilder mediatorBuilder)
        {
            _mediatorBuilder = mediatorBuilder;
            _methods = new MethodCollection<Method<Action<object, object>>> ();
            _direct = null!;
        }

        public IPipelineBuilder<TRequest> Call<THandler>(Action<THandler, TRequest> action)
        {
            Action<object, object> typedHandler = (h, r) => action((THandler) h, (TRequest) r);
            var method = new Method<Action<object, object>>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IMediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, TResult> func)
        {
            _direct = new Direct<TRequest, TResult, THandler>(func);
            return _mediatorBuilder;
        }

        public void Publish(GetService getService, object request)
        {
            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                handler.Action(concreteHandler, (TRequest) request);
            }
        }

        public TResult Send<TResult>(GetService getService, object request)
        {
            if (_direct is null)
            {
                throw new ReturnFunctionIsNullException("The return function is null. Send<TResult> method not executed.");
            }

            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                handler.Action(concreteHandler, (TRequest) request);
            }

            return _direct.Send<TResult>(getService, request!) !;
        }

        public IMediatorBuilder Build()
        {
            return _mediatorBuilder;
        }
    }
}