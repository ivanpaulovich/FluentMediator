using FluentMediator.Pipelines;

namespace FluentMediator
{
    public interface IPipelineProviderBuilder : 
        Pipelines.Pipeline.IPipelineProviderBuilder,
        Pipelines.PipelineAsync.IPipelineProviderBuilder,
        Pipelines.CancellablePipelineAsync.IPipelineProviderBuilder
    {
        IPipelineBehavior<TRequest> On<TRequest>();        
        IPipelineProvider Build();
    }
}