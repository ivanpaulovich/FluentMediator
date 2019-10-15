using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public class CancellablePipelineBuilder<TRequest> : ICancellablePipelineBuilder<TRequest>
    {
        private readonly IMethodCollection<Method<Func<object, object, CancellationToken, Task>>> _methods;
        private ICancellableAsync _direct;

        public CancellablePipelineBuilder()
        {
            _methods = new MethodCollection<Method<Func<object, object, CancellationToken, Task>>>();
            _direct = null!;
        }

        public ICancellablePipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> func)
        {
            Func<object, object, CancellationToken, Task> typedHandler = async(h, r, c) => await func((THandler) h, (TRequest) r, c);
            var method = new Method<Func<object, object, CancellationToken, Task>>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public ICancellablePipeline Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> func)
        {
            _direct = new CancellableAsyncDirect<TRequest, TResult, THandler>(func);
            return Build();
        }

        public ICancellablePipeline Build()
        {
            return new CancellablePipeline(_methods, _direct, typeof(TRequest));
        }
    }
}