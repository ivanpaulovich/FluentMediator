using FluentMediator.Pipelines.PipelineAsync;
using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public interface IPipelineBehavior<TRequest>
    {
        IPipelineBuilder<TRequest> Pipeline(string? pipelineName = null);
        IPipelineAsyncBuilder<TRequest> AsyncPipeline(string? pipelineName = null);
        ICancellablePipelineAsyncBuilder<TRequest> AsyncCancellablePipeline(string? pipelineName = null);
    }
}