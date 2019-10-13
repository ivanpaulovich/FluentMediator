using System;

namespace FluentMediator.Pipelines.Pipeline
{
    public class Pipeline<TRequest> : IPipeline
    {
        private readonly IMediatorBuilder _pipelinesManager;
        private readonly IMethodCollection<Method<Action<object, TRequest>, TRequest>, TRequest > _methods;
        private IDirect _direct;

        public Pipeline(IMediatorBuilder pipelinesManager)
        {
            _pipelinesManager = pipelinesManager;
            _methods = new MethodCollection<Method<Action<object, TRequest>, TRequest>, TRequest > ();
            _direct = null!;
        }

        public Pipeline<TRequest> Call<THandler>(Action<THandler, TRequest> action)
        {
            Action<object, TRequest> typedHandler = (h, r) => action((THandler) h, (TRequest) r);
            var method = new Method<Action<object, TRequest>, TRequest>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IDirect Return<TResult, THandler>(Func<THandler, TRequest, TResult> func)
        {
            _direct = new Direct<TRequest, TResult, THandler>(func);
            return _direct;
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
            return _pipelinesManager;
        }
    }
}