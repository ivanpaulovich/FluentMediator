using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public interface IPipelineBehavior<TRequest>
    {
        IPipelineBuilder<TRequest> Pipeline();
        IAsyncPipelineBuilder<TRequest> AsyncPipeline();
        ICancellablePipelineBuilder<TRequest> CancellablePipeline();
    }
}