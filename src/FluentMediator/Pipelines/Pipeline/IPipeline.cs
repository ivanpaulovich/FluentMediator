namespace FluentMediator.Pipelines.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPipeline : INamedPipeline, ITypedPipeline
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getService"></param>
        /// <param name="request"></param>
        void Publish(GetService getService, object request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getService"></param>
        /// <param name="request"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        TResult Send<TResult>(GetService getService, object request);
    }
}