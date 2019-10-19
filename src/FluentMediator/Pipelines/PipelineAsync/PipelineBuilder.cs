using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    internal sealed class PipelineBuilder<TRequest> : IPipelineAsyncBuilder<TRequest>
    {
        private readonly IMethodCollection<Method<Func<object, object, Task>>> _methods;
        private IDirect? _direct;
        private string? _name;

        public PipelineBuilder(string? name)
        {
            _methods = new MethodCollection<Method<Func<object, object, Task>>>();
            _name = name;
        }

        public IPipelineAsyncBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, Task> func)
        {
            Func<object, object, Task> typedHandler = async(h, r) => await func((THandler) h, (TRequest) r);
            var method = new Method<Func<object, object, Task>>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IPipelineAsync Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> func)
        {
            _direct = new Direct<TRequest, TResult, THandler>(func);
            return Build();
        }

        public IPipelineAsync Build()
        {
            return new Pipeline(_methods, _direct, typeof(TRequest), _name);
        }
    }
}