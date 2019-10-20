using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    internal class PipelineBuilder<TRequest> : ICancellablePipelineAsyncBuilder<TRequest>
    {
        private readonly IMethodCollection<Method<Func<object, object, CancellationToken, Task>>> _methods;
        private IDirect? _direct;
        private string? _name;

        public PipelineBuilder(string? name)
        {
            _methods = new MethodCollection<Method<Func<object, object, CancellationToken, Task>>>();
            _name = name;
        }

        public ICancellablePipelineAsyncBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> func)
        {
            Func<object, object, CancellationToken, Task> typedHandler = async(h, r, c) => await func((THandler) h, (TRequest) r, c);
            var method = new Method<Func<object, object, CancellationToken, Task>>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public ICancellablePipelineAsync Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> func)
        {
            _direct = new Direct<TRequest, TResult, THandler>(func);
            return Build();
        }

        public ICancellablePipelineAsync Build()
        {
            return new Pipeline(_methods, _direct, typeof(TRequest), _name);
        }
    }
}