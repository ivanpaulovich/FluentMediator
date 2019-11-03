namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICancellablePipelineProviderBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineBuilder"></param>
        /// <returns></returns>
        ICancellablePipelineAsyncBuilder Add(ICancellablePipelineAsyncBuilder pipelineBuilder);
    }
}