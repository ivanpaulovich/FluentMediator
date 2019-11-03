using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    /// <summary>
    /// Cancellable Mediator
    /// </summary>
    public interface ICancellableMediator
    {
        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="request">A message</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="pipelineName">Pipeline Name</param>
        /// <returns>Task object</returns>
        Task PublishAsync(object request, CancellationToken cancellationToken, string? pipelineName = null);
        
        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="request">A message</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="pipelineName">Pipeline Name</param>
        /// <typeparam name="TResult">Result Type</typeparam>
        /// <returns></returns>
        Task<TResult> SendAsync<TResult>(object request, CancellationToken cancellationToken, string? pipelineName = null);
    }
}