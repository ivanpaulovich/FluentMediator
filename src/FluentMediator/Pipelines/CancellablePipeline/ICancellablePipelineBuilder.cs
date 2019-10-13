using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public interface ICancellablePipelineBuilder<TRequest>
    {
        ICancellablePipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> func);
        IMediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> func);
        IMediatorBuilder Build();
    }
}