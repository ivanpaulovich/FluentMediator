using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator.Pipelines
{
    public interface IPipelineBehavior<TRequest>
    {
        IPipelineBuilder<TRequest> Pipeline(string? pipelineName = null);
        IPipelineAsyncBuilder<TRequest> PipelineAsync(string? pipelineName = null);
        ICancellablePipelineAsyncBuilder<TRequest> CancellablePipelineAsync(string? pipelineName = null);
    }
}