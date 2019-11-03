namespace FluentMediator.Pipelines.PipelineAsync
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncPipelineProviderBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineBuilder"></param>
        /// <returns></returns>
        IPipelineAsyncBuilder Add(IPipelineAsyncBuilder pipelineBuilder);
    }
}