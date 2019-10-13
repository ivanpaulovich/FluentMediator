using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public interface IMediatorBuilder
    {
        IPipelineBuilder<TRequest> On<TRequest>();

        Pipeline<TRequest> AddPipeline<TRequest>(Pipeline<TRequest> pipeline);
        AsyncPipeline<TRequest> AddAsyncPipeline<TRequest>(AsyncPipeline<TRequest> asyncPipeline);
        CancellablePipeline<TRequest> AddCancellablePipeline<TRequest>(CancellablePipeline<TRequest> cancellablePipeline);

        IMediator Build(GetService getService);
    }
}