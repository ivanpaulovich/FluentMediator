using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public interface IPipelineBuilder<TRequest>
    {
        Pipeline<TRequest> Pipeline();
        AsyncPipeline<TRequest> AsyncPipeline();
        CancellablePipeline<TRequest> CancellablePipeline();
    }
}