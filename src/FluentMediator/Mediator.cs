namespace FluentMediator
{
    public partial class Mediator : IMediator
    {
        public GetService GetService { get; }

        public Mediator(GetService getService)
        {
            GetService = getService;
            PipelineCollection = new PipelineCollection<IPipeline>();
            AsyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            CancellablePipelineCollection = new PipelineCollection<ICancellablePipeline>();
        }

        public PipelineBuilder<Request> When<Request>()
        {
            return new PipelineBuilder<Request>(this);
        }
    }
}