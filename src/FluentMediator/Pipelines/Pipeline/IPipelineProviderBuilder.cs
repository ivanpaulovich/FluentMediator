namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipelineProviderBuilder
    {
        IPipelineBuilder Add(IPipelineBuilder pipelineBuilder);
    }
}