using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    /// <summary>
    /// Cancellable Pipeline
    /// </summary>
    public interface ICancellablePipelineAsync : INamedPipeline, ITypedPipeline
    {
        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="getService">Retriever a service from the container</param>
        /// <param name="request">Request message</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task object</returns>
        Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="getService">Retriever a service from the container</param>
        /// <param name="request">Request message</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <typeparam name="TResult">Message type</typeparam>
        /// <returns>Response message</returns>
        Task<TResult> SendAsync<TResult>(GetService getService, object request, CancellationToken cancellationToken);
    }
}