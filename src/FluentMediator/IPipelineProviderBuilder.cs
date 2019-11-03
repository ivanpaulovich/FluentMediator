using FluentMediator.Pipelines;
using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator
{
    /// <summary>
    /// Builds pipelines for a specific message
    /// </summary>
    public interface IPipelineProviderBuilder:
        ISyncPipelineProviderBuilder,
        IAsyncPipelineProviderBuilder,
        ICancellablePipelineProviderBuilder
        {
            /// <summary>
            /// Begin building a pipeline for a specific message
            /// </summary>
            /// <typeparam name="TRequest">Message Type</typeparam>
            /// <returns>A more specific PipelineBehavior</returns>
            IPipelineBehavior<TRequest> On<TRequest>();

            /// <summary>
            /// Builds the pipeline
            /// </summary>
            /// <returns>An immutable Pipeline Provider</returns>
            IPipelineProvider Build();
        }
}