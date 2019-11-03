using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IPipelineAsyncBuilder<TRequest> : IPipelineAsyncBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        IPipelineAsyncBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, Task> func);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        IPipelineAsync Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> func);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPipelineAsyncBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IPipelineAsync Build();
    }
}