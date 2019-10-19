using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    public interface ICancellablePipelineAsyncBuilder<TRequest> : ICancellablePipelineAsyncBuilder
    {
        ICancellablePipelineAsyncBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> func);
        ICancellablePipelineAsync Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> func);
    }

    public interface ICancellablePipelineAsyncBuilder
    {
        ICancellablePipelineAsync Build();
    }
}