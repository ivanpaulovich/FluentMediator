using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    internal sealed class AsyncPipelineBuilder<TRequest> : IAsyncPipelineBuilder<TRequest>
    {
        private readonly IMethodCollection<Method<Func<object, object, Task>>> _methods;
        private IDirectAsync? _direct;
        private string? _name;

        public AsyncPipelineBuilder(string? name)
        {
            _methods = new MethodCollection<Method<Func<object, object, Task>>>();
            _name = name;
        }

        public IAsyncPipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, Task> func)
        {
            Func<object, object, Task> typedHandler = async(h, r) => await func((THandler) h, (TRequest) r);
            var method = new Method<Func<object, object, Task>>(typeof(THandler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IAsyncPipeline Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> func)
        {
            _direct = new DirectAsync<TRequest, TResult, THandler>(func);
            return Build();
        }

        public IAsyncPipeline Build()
        {
            return new AsyncPipeline(_methods, _direct, typeof(TRequest), _name);
        }
    }
}