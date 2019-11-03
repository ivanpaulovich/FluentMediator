using System;

namespace FluentMediator.Pipelines.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISyncPipelineProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        IPipeline GetPipeline(Type requestType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineName"></param>
        /// <returns></returns>
        IPipeline GetPipeline(string pipelineName);
    }
}