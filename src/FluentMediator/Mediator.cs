using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public class Mediator
    {
        private readonly GetService _getService;
        private PipelineCollection _pipelineCollection;
        private AsyncPipelineCollection _asyncPipelineCollection;
        private CancellableAsyncPipelineCollection _cancellableAsyncPipelineCollection;

        public Mediator(GetService getService)
        {
            _getService = getService;
            _pipelineCollection = new PipelineCollection();
            _asyncPipelineCollection = new AsyncPipelineCollection();
            _cancellableAsyncPipelineCollection = new CancellableAsyncPipelineCollection();
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
            IPipeline pipeline;
            if (_pipelineCollection.Contains<Request>(out pipeline))
            {
                pipeline.Publish(request!);
            }
        }

        public async Task PublishAsync<Request>(Request request)
        {
            IAsyncPipeline asyncPipeline;
            if (_asyncPipelineCollection.Contains<Request>(out asyncPipeline))
            {
                await asyncPipeline.PublishAsync(request!);
            }
        }

        public async Task PublishAsync<Request>(Request request, CancellationToken ct)
        {
            ICancellableAsyncPipeline cancellableAsyncPipeline;
            if (_cancellableAsyncPipelineCollection.Contains<Request>(out cancellableAsyncPipeline))
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