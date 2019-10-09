using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public interface IMediator : IPipelineMediator, IAsyncPipelineMediator, ICancellablePipelineMediator
    {
        GetService GetService { get; }
    }
}