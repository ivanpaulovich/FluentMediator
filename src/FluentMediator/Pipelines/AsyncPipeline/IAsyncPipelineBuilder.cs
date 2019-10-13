using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IAsyncPipelineBuilder<TRequest>
    {
        IAsyncPipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, Task> func);
        IMediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> func);
        IMediatorBuilder Build();
    }
}