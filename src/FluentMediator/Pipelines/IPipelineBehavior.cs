using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator.Pipelines
{
    /// <summary>
    /// PipelineBehavior
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IPipelineBehavior<TRequest>
    {
        /// <summary>
        /// Creates a IPipelineBuilder
        /// </summary>
        /// <param name="pipelineName">An unique name or null</param>
        /// <returns>IPipelineBuilder</returns>
        IPipelineBuilder<TRequest> Pipeline(string? pipelineName = null);

        /// <summary>
        /// Creates an IPipelineAsyncBuilder
        /// </summary>
        /// <param name="pipelineName">An unique name or null</param>
        /// <returns>IPipelineAsyncBuilder</returns>
        IPipelineAsyncBuilder<TRequest> PipelineAsync(string? pipelineName = null);

        /// <summary>
        /// Creates an ICancellablePipelineAsyncBuilder
        /// </summary>
        /// <param name="pipelineName">An unique name or null</param>
        /// <returns>ICancellablePipelineAsyncBuilder</returns>
        ICancellablePipelineAsyncBuilder<TRequest> CancellablePipelineAsync(string? pipelineName = null);
    }
}