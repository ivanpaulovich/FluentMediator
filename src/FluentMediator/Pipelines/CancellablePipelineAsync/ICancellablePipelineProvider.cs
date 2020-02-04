using System;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICancellablePipelineProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        ICancellablePipelineAsync GetCancellablePipeline(Type requestType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineName"></param>
        /// <returns></returns>
        ICancellablePipelineAsync GetCancellablePipeline(string pipelineName);
    }
}