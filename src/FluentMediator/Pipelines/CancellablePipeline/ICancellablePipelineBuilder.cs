using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public interface ICancellablePipelineBuilder<TRequest> : ICancellablePipelineBuilder
    {
        ICancellablePipelineBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> func);
        ICancellablePipeline Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> func);
    }

    public interface ICancellablePipelineBuilder
    {
        ICancellablePipeline Build();
    }
}