using System.Threading;
using System.Threading.Tasks;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public sealed class Mediator : IMediator
    {
        public GetService GetService { get; }
        private PipelineCollection<IPipeline> _pipelineCollection { get; }
        private PipelineCollection<IAsyncPipeline> _asyncPipelineCollection { get; }
        private PipelineCollection<ICancellablePipeline> _cancellablePipelineCollection { get; }

        public Mediator(
            GetService getService,
            PipelineCollection<IPipeline> pipelineCollection,
            PipelineCollection<IAsyncPipeline> asyncPipelineCollection,
            PipelineCollection<ICancellablePipeline> cancellablePipelineCollection)
        {
            GetService = getService;
            _pipelineCollection = pipelineCollection;
            _asyncPipelineCollection = asyncPipelineCollection;
            _cancellablePipelineCollection = cancellablePipelineCollection;
        }

        public void Publish(object request)
        {
            var pipeline = _pipelineCollection.Get(request);
            pipeline.Publish(GetService, request!);
        }

        public Response Send<Response>(object request)
        {
            var pipeline = _pipelineCollection.Get(request);
            return pipeline.Send<Response>(GetService, request!);
        }

        public async Task PublishAsync(object request)
        {
            var pipeline = _asyncPipelineCollection.Get(request);
            await pipeline.PublishAsync(GetService, request!);
        }

        public async Task<Response> SendAsync<Response>(object request)
        {
            var pipeline = _asyncPipelineCollection.Get(request);
            return await pipeline.SendAsync<Response>(GetService, request!);
        }

        public async Task PublishAsync(object request, CancellationToken ct)
        {
            var pipeline = _cancellablePipelineCollection.Get(request);
            await pipeline.PublishAsync(GetService, request!, ct);
        }

        public async Task<Response> SendAsync<Response>(object request, CancellationToken ct)
        {
            var pipeline = _cancellablePipelineCollection.Get(request);
            return await pipeline.SendAsync<Response>(GetService, request!, ct);
        }
    }
}