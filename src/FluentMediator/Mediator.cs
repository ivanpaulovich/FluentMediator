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

        public void Publish(object request)
        {
            var pipeline = _pipelines.GetPipeline(request.GetType());
            pipeline.Publish(GetService, request!);
        }

        public TResult Send<TResult>(object request)
        {
            var pipeline = _pipelines.GetPipeline(request.GetType());
            return pipeline.Send<TResult>(GetService, request!);
        }

        public async Task PublishAsync(object request)
        {
            var pipeline = _pipelines.GetAsyncPipeline(request.GetType());
            await pipeline.PublishAsync(GetService, request!);
        }

        public async Task<TResult> SendAsync<TResult>(object request)
        {
            var pipeline = _pipelines.GetAsyncPipeline(request.GetType());
            return await pipeline.SendAsync<TResult>(GetService, request!);
        }

        public async Task PublishAsync(object request, CancellationToken ct)
        {
            var pipeline = _pipelines.GetCancellablePipeline(request.GetType());
            await pipeline.PublishAsync(GetService, request!, ct);
        }

        public async Task<TResult> SendAsync<TResult>(object request, CancellationToken ct)
        {
            var pipeline = _pipelines.GetCancellablePipeline(request.GetType());
            return await pipeline.SendAsync<TResult>(GetService, request!, ct);
        }
    }
}