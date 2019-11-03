using System;

namespace FluentMediator.Pipelines.PipelineAsync
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncPipelineProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        IPipelineAsync GetAsyncPipeline(Type requestType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineName"></param>
        /// <returns></returns>
        IPipelineAsync GetAsyncPipeline(string pipelineName);
    }
}