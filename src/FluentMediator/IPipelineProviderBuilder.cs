using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public interface IPipelineProviderBuilder
    {
        IPipelineBehavior<TRequest> On<TRequest>();
        IPipelineBuilder Add(IPipelineBuilder pipeline);
        IAsyncPipelineBuilder Add(IAsyncPipelineBuilder asyncPipeline);
        ICancellablePipelineBuilder Add(ICancellablePipelineBuilder cancellablePipeline);
        IPipelineProvider Build();
    }
}