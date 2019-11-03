namespace FluentMediator.Pipelines.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISyncMediator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pipelineName"></param>
        void Publish(object request, string? pipelineName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pipelineName"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        TResult Send<TResult>(object request, string? pipelineName = null);
    }
}