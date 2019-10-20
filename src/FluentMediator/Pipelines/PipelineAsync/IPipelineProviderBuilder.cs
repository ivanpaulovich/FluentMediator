namespace FluentMediator.Pipelines.PipelineAsync
{
    public interface IPipelineProviderBuilder
    {
         IPipelineAsyncBuilder Add(IPipelineAsyncBuilder pipelineBuilder);
    }
}