using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator
{
    /// <summary>
    /// Retrieves a Pipeline for a specific Message
    /// </summary>
    public interface IPipelineProvider:
        ISyncPipelineProvider,
        IAsyncPipelineProvider,
        ICancellablePipelineProvider { }
}