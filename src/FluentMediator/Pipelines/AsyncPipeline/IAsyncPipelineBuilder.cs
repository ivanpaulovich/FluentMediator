using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IAsyncPipelineBuilder<TRequest> : IAsyncPipelineBuilder
    {
        IAsyncPipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, Task> func);
        IAsyncPipeline Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> func);
    }

    public interface IAsyncPipelineBuilder
    {
        IAsyncPipeline Build();
    }
}