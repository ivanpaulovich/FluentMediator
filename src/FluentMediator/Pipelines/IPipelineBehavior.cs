using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public interface IPipelineBehavior<TRequest>
    {
        IPipelineBuilder<TRequest> Pipeline(string? name = null);
        IAsyncPipelineBuilder<TRequest> AsyncPipeline(string? name = null);
        ICancellablePipelineBuilder<TRequest> CancellablePipeline(string? name = null);
    }
}