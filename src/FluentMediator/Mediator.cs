using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMediator.Pipeline;
using FluentMediator.AsyncPipeline;
using FluentMediator.CancellableAsyncPipeline;

namespace FluentMediator
{
    public class Mediator
    {
        private readonly GetService _getService;
        private PipelineCollection<IPipeline> _pipelineCollection;
        private PipelineCollection<IAsyncPipeline> _asyncPipelineCollection;
        private PipelineCollection<ICancellableAsyncPipeline> _cancellableAsyncPipelineCollection;

        public Mediator(GetService getService)
        {
            _getService = getService;
            _pipelineCollection = new PipelineCollection<IPipeline>();
            _asyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            _cancellableAsyncPipelineCollection = new PipelineCollection<ICancellableAsyncPipeline>();
        }

        public Pipeline<Request> Pipeline<Request>()
        {
            var pipeline = new Pipeline<Request>(this);
            _pipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<Request> AsyncPipeline<Request>()
        {
            var asyncPipeline = new AsyncPipeline<Request>(this);
            _asyncPipelineCollection.Add<Request>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellableAsyncPipeline<Request> CancellableAsyncPipeline<Request>()
        {
            var cancellableAsyncPipeline = new CancellableAsyncPipeline<Request>(this);
            _cancellableAsyncPipelineCollection.Add<Request>(cancellableAsyncPipeline);
            return cancellableAsyncPipeline;
        }

        public void Publish<Request>(Request request)
        {
            if (_pipelineCollection.Contains<Request>(out var pipeline))
            {
                pipeline.Publish(request!);
            }
        }

        public async Task PublishAsync<Request>(Request request)
        {
            if (_asyncPipelineCollection.Contains<Request>(out var asyncPipeline))
            {
                await asyncPipeline.PublishAsync(request!);
            }
        }

        public async Task PublishAsync<Request>(Request request, CancellationToken ct)
        {
            if (_cancellableAsyncPipelineCollection.Contains<Request>(out var cancellableAsyncPipeline))
            {
                await cancellableAsyncPipeline.PublishAsync(request!, ct);
            }
        }

        internal object GetConcreteHandler(Type handlerType)
        {
            return _getService(handlerType);
        }
    }
}