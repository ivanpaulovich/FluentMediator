namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    public interface IPipelineProviderBuilder
    {
        ICancellablePipelineAsyncBuilder Add(ICancellablePipelineAsyncBuilder pipelineBuilder);
    }
}