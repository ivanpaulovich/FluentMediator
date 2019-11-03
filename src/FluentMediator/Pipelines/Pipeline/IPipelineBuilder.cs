using System;

namespace FluentMediator.Pipelines.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IPipelineBuilder<TRequest> : IPipelineBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        IPipelineBuilder<TRequest> Call<THandler>(Action<THandler, TRequest> action);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        IPipeline Return<TResult, THandler>(Func<THandler, TRequest, TResult> func);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPipelineBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IPipeline Build();
    }
}