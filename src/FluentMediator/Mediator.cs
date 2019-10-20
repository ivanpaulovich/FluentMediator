using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public sealed class Mediator : IMediator
    {
        public GetService GetService { get; }
        private IPipelineProvider _pipelines;

        public Mediator(
            GetService getService,
            IPipelineProvider pipelines)
        {
            GetService = getService;
            _pipelines = pipelines;
        }

        public void Publish(object request, string? pipelineName = null)
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

        public TResult Send<TResult>(object request, string? pipelineName = null)
        {
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

        public async Task PublishAsync(object request, string? pipelineName = null)
        {
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

        public async Task<TResult> SendAsync<TResult>(object request, string? pipelineName = null)
        {
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

        public async Task PublishAsync(object request, CancellationToken ct, string? pipelineName = null)
        {
            if (pipelineName is string)
            {
                var pipeline = _pipelines.GetCancellablePipeline(pipelineName);
                await pipeline.PublishAsync(GetService, request, ct);
            }
            else
            {
                var pipeline = _pipelines.GetCancellablePipeline(request.GetType());
                await pipeline.PublishAsync(GetService, request, ct);
            }
        }

        public async Task<TResult> SendAsync<TResult>(object request, CancellationToken ct, string? pipelineName = null)
        {
            if (pipelineName is string)
            {
                var pipeline = _pipelines.GetCancellablePipeline(pipelineName);
                return await pipeline.SendAsync<TResult>(GetService, request, ct);
            }
            else
            {
                var pipeline = _pipelines.GetCancellablePipeline(request.GetType());
                return await pipeline.SendAsync<TResult>(GetService, request, ct);
            }
        }
    }
}