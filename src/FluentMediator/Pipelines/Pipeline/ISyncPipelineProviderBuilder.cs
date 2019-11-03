namespace FluentMediator.Pipelines.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISyncPipelineProviderBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineBuilder"></param>
        /// <returns></returns>
        IPipelineBuilder Add(IPipelineBuilder pipelineBuilder);
    }
}