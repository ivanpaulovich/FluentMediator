using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public interface IMediatorBuilder
    {
        IPipelineBehavior<TRequest> On<TRequest>();
        IPipelineBuilder<TRequest> AddPipeline<TRequest>(Pipeline<TRequest> pipeline);
        IAsyncPipelineBuilder<TRequest> AddAsyncPipeline<TRequest>(AsyncPipeline<TRequest> asyncPipeline);
        ICancellablePipelineBuilder<TRequest> AddCancellablePipeline<TRequest>(CancellablePipeline<TRequest> cancellablePipeline);
        IMediator Build(GetService getService);
    }
}