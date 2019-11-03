using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncMediator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pipelineName"></param>
        /// <returns></returns>
        Task PublishAsync(object request, string? pipelineName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pipelineName"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        Task<TResult> SendAsync<TResult>(object request, string? pipelineName = null);
    }
}