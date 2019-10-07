namespace FluentMediator
{
    public partial class Mediator : IMediator
    {
        public GetService GetService { get; }

        public Mediator(GetService getService)
        {
            GetService = getService;
            PipelineCollection = new PipelineCollection<IPipeline>();
            _asyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            _cancellablePipelineCollection = new PipelineCollection<ICancellablePipeline>();
            _sendPipelineCollection = new PipelineCollection<IDirect>();
            _sendAsyncPipelineCollection = new PipelineCollection<IDirectAsync>();
        }

        public PipelineBuilder<Request> For<Request>()
        {
            return new PipelineBuilder<Request>(this);
        }
    }
}