namespace FluentMediator
{
    public interface IPipelineProvider : 
        Pipelines.Pipeline.IPipelineProvider,
        Pipelines.PipelineAsync.IPipelineProvider,
        Pipelines.CancellablePipelineAsync.IPipelineProvider { }
}