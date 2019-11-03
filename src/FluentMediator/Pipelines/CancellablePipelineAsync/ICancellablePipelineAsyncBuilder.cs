using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    /// <summary>
    /// Cancellable Pipeline
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface ICancellablePipelineAsyncBuilder<TRequest> : ICancellablePipelineAsyncBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        ICancellablePipelineAsyncBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, CancellationToken, Task> func);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        ICancellablePipelineAsync Return<TResult, THandler>(Func<THandler, TRequest, CancellationToken, Task<TResult>> func);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICancellablePipelineAsyncBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICancellablePipelineAsync Build();
    }
}