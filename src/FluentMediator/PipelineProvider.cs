using System;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator
{
    /// <summary>
    /// Retrieves Pipelines for a message
    /// </summary>
    internal sealed class PipelineProvider : IPipelineProvider
    {
        private readonly IPipelineCollection<IPipeline> _pipelineCollection;
        private readonly IPipelineCollection<IPipelineAsync> _asyncPipelineCollection;
        private readonly IPipelineCollection<ICancellablePipelineAsync> _cancellablePipelineCollection;

        /// <summary>
        /// Instantiate a PipelineProvider
        /// </summary>
        /// <param name="pipelineCollection">Sync Pipelines</param>
        /// <param name="asyncPipelineCollection">Async Pipelines</param>
        /// <param name="cancellablePipelineCollection">Cancellable Pipelines</param>
        public PipelineProvider(
            IPipelineCollection<IPipeline> pipelineCollection,
            IPipelineCollection<IPipelineAsync> asyncPipelineCollection,
            IPipelineCollection<ICancellablePipelineAsync> cancellablePipelineCollection)
        {
            _pipelineCollection = pipelineCollection;
            _asyncPipelineCollection = asyncPipelineCollection;
            _cancellablePipelineCollection = cancellablePipelineCollection;
        }

        /// <summary>
        /// Gets an async pipeline
        /// </summary>
        /// <param name="requestType">Message Type</param>
        /// <returns>Pipeline</returns>
        public IPipelineAsync GetAsyncPipeline(Type requestType)
        {
            return _asyncPipelineCollection.Get(requestType);
        }

        /// <summary>
        /// Gets an async pipeline
        /// </summary>
        /// <param name="pipelineName">Pipeline Name</param>
        /// <returns>Pipeline</returns>
        public IPipelineAsync GetAsyncPipeline(string pipelineName)
        {
            return _asyncPipelineCollection.Get(pipelineName);
        }

        /// <summary>
        /// Gets a cancellable pipeline
        /// </summary>
        /// <param name="requestType">Request Type</param>
        /// <returns>Pipeline</returns>
        public ICancellablePipelineAsync GetCancellablePipeline(Type requestType)
        {
            return _cancellablePipelineCollection.Get(requestType);
        }

        /// <summary>
        /// Gets a cancellable pipeline
        /// </summary>
        /// <param name="pipelineName">Pipeline Name</param>
        /// <returns>Pipeline</returns>
        public ICancellablePipelineAsync GetCancellablePipeline(string pipelineName)
        {
            return _cancellablePipelineCollection.Get(pipelineName);
        }

        /// <summary>
        /// Gets a Pipeline
        /// </summary>
        /// <param name="requestType">Request Type</param>
        /// <returns>Pipeline</returns>
        public IPipeline GetPipeline(Type requestType)
        {
            return _pipelineCollection.Get(requestType);
        }

        /// <summary>
        /// Gets a Pipeline
        /// </summary>
        /// <param name="pipelineName">Pipeline Name</param>
        /// <returns>Pipeline</returns>
        public IPipeline GetPipeline(string pipelineName)
        {
            return _pipelineCollection.Get(pipelineName);
        }
    }
}