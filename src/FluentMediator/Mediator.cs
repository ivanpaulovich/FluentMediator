namespace FluentMediator
{
    public partial class Mediator : IMediator
    {
        public GetService GetService { get; }
        private PipelineCollection<IPipeline> PipelineCollection { get; }
        private PipelineCollection<IAsyncPipeline> AsyncPipelineCollection { get; }
        private PipelineCollection<ICancellablePipeline> CancellablePipelineCollection { get; }

        public Mediator(
            GetService getService,
            PipelineCollection<IPipeline> pipelineCollection,
            PipelineCollection<IAsyncPipeline> asyncPipelineCollection,
            PipelineCollection<ICancellablePipeline> cancellablePipelineCollection)
        {
            GetService = getService;
            PipelineCollection = pipelineCollection;
            AsyncPipelineCollection = asyncPipelineCollection;
            CancellablePipelineCollection = cancellablePipelineCollection;
        }
    }
}