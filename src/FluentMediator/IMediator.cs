using System;
using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator
{
    /// <summary>
    /// Publishes/Sends messages through the Pipelines
    /// </summary>
    public interface IMediator:
        ISyncMediator,
        IAsyncMediator,
        ICancellableMediator
    {
        /// <summary>
        /// On Pipeline Not Found Event Handler.
        /// </summary>
        event EventHandler<PipelineNotFoundEventArgs>? PipelineNotFound;
    }
}