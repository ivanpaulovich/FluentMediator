using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMediator.Pipelines;

namespace FluentMediator
{
    /// <summary>
    /// Publishes/Sends messages through the Pipelines
    /// </summary>
    public class Mediator : IMediator
    {
        /// <summary>
        /// Returns a service from the Container
        /// </summary>
        /// <value></value>
        public GetService GetService { get; }

        private IPipelineProvider _pipelines;

        /// <summary>
        /// On Pipeline Not Found Handler.
        /// </summary>
        public event EventHandler<PipelineNotFoundEventArgs>? PipelineNotFound;

        /// <summary>
        /// Instantiate a Mediator
        /// </summary>
        /// <param name="getService">Service Provider</param>
        /// <param name="pipelines">Pipeline Provider</param>
        public Mediator(
            GetService getService,
            IPipelineProvider pipelines)
        {
            GetService = getService;
            _pipelines = pipelines;
        }

        /// <summary>
        /// Publishes messages through the Pipeline
        /// </summary>
        /// <param name="request">Message</param>
        /// <param name="pipelineName">An optional pipeline name</param>
        public void Publish(object request, string? pipelineName = null)
        {
            if (request is null)
            {
                throw new NullRequestException("The request is null.");
            }

            try
            {
                if (pipelineName is string)
                {
                    var pipeline = _pipelines.GetPipeline(pipelineName);
                    pipeline.Publish(GetService, request!);
                }
                else
                {
                    var pipeline = _pipelines.GetPipeline(request.GetType());
                    pipeline.Publish(GetService, request!);
                }
            }
            catch (PipelineNotFoundException)
            {
                var e = new PipelineNotFoundEventArgs(request);
                OnPipelineNotFound(e);
            }
        }

        /// <summary>
        /// Publishes messages through the Pipeline
        /// </summary>
        /// <param name="request">Message</param>
        /// <param name="pipelineName">An optional pipeline name</param>
        /// <typeparam name="TResult">The desired Typed result</typeparam>
        /// <returns>The result object</returns>
        public TResult Send<TResult>(object request, string? pipelineName = null)
        {
            if (request is null)
            {
                throw new NullRequestException("The request is null.");
            }

            if (pipelineName is string)
            {
                var pipeline = _pipelines.GetPipeline(pipelineName);
                return pipeline.Send<TResult>(GetService, request);
            }
            else
            {
                var pipeline = _pipelines.GetPipeline(request.GetType());
                return pipeline.Send<TResult>(GetService, request);
            }
        }

        /// <summary>
        /// Publishes messages through the Pipeline
        /// </summary>
        /// <param name="request">Message</param>
        /// <param name="pipelineName">An optional pipeline name</param>
        /// <returns>Task object</returns>
        public async Task PublishAsync(object request, string? pipelineName = null)
        {
            if (request is null)
            {
                throw new NullRequestException("The request is null.");
            }

            if (pipelineName is string)
            {
                var pipeline = _pipelines.GetAsyncPipeline(pipelineName);
                await pipeline.PublishAsync(GetService, request);
            }
            else
            {
                var pipeline = _pipelines.GetAsyncPipeline(request.GetType());
                await pipeline.PublishAsync(GetService, request);
            }
        }

        /// <summary>
        /// Publishes messages through the Pipeline
        /// </summary>
        /// <param name="request">Message</param>
        /// <param name="pipelineName">An optional pipeline name</param>
        /// <typeparam name="TResult">The desired Typed result</typeparam>
        /// <returns>The result object</returns>
        public async Task<TResult> SendAsync<TResult>(object request, string? pipelineName = null)
        {
            if (request is null)
            {
                throw new NullRequestException("The request is null.");
            }

            if (pipelineName is string)
            {
                var pipeline = _pipelines.GetAsyncPipeline(pipelineName);
                return await pipeline.SendAsync<TResult>(GetService, request);
            }
            else
            {
                var pipeline = _pipelines.GetAsyncPipeline(request.GetType());
                return await pipeline.SendAsync<TResult>(GetService, request);
            }
        }

        /// <summary>
        /// Publishes messages through the Pipeline
        /// </summary>
        /// <param name="request">Message</param>
        /// <param name="cancellationToken">Cancellation Token to gracefully exit in middle of execution</param>
        /// <param name="pipelineName">Optional Pipeline Name</param>
        /// <returns>Task object</returns>
        public async Task PublishAsync(object request, CancellationToken cancellationToken, string? pipelineName = null)
        {
            if (request is null)
            {
                throw new NullRequestException("The request is null.");
            }

            if (pipelineName is string)
            {
                var pipeline = _pipelines.GetCancellablePipeline(pipelineName);
                await pipeline.PublishAsync(GetService, request, cancellationToken);
            }
            else
            {
                var pipeline = _pipelines.GetCancellablePipeline(request.GetType());
                await pipeline.PublishAsync(GetService, request, cancellationToken);
            }
        }

        /// <summary>
        /// Sends messages through the Pipeline
        /// </summary>
        /// <param name="request">Message</param>
        /// <param name="cancellationToken">Cancellation Token to gracefully exit in middle of execution</param>
        /// <param name="pipelineName">Optional Pipeline Name</param>
        /// <typeparam name="TResult">Result Type</typeparam>
        /// <returns>Result object</returns>
        public async Task<TResult> SendAsync<TResult>(object request, CancellationToken cancellationToken, string? pipelineName = null)
        {
            if (request is null)
            {
                throw new NullRequestException("The request is null.");
            }

            if (pipelineName is string)
            {
                var pipeline = _pipelines.GetCancellablePipeline(pipelineName);
                return await pipeline.SendAsync<TResult>(GetService, request, cancellationToken);
            }
            else
            {
                var pipeline = _pipelines.GetCancellablePipeline(request.GetType());
                return await pipeline.SendAsync<TResult>(GetService, request, cancellationToken);
            }
        }

        /// <summary>
        /// On Pipeline Not Found.
        /// </summary>
        /// <param name="e">OnErrorEventArgs.</param>
        protected virtual void OnPipelineNotFound(PipelineNotFoundEventArgs e)
        {
            if (this.PipelineNotFound is EventHandler<PipelineNotFoundEventArgs>)
            {
                this.PipelineNotFound(this, e);
            }
        }
    }
}