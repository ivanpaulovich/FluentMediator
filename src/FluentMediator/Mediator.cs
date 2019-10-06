namespace FluentMediator
{
    public partial class Mediator : IMediator
    {
        internal GetService GetService { get; }
        private readonly PipelineCollection<IPipeline> _pipelineCollection;

        public Mediator(GetService getService)
        {
            GetService = getService;
            _pipelineCollection = new PipelineCollection<IPipeline>();
            _asyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            _cancellablePipelineCollection = new PipelineCollection<ICancellablePipeline>();
            _sendPipelineCollection = new PipelineCollection<IDirect>();
            _sendAsyncPipelineCollection = new PipelineCollection<IDirectAsync>();
        }

        public Pipeline<Request> Pipeline<Request>()
        {
            var pipeline = new Pipeline<Request>(this);
            _pipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public void Publish<Request>(Request request)
        {
            if (_pipelineCollection.Contains<Request>(out var pipeline))
            {
                pipeline?.Publish(request!);
            }
        }
    }
}