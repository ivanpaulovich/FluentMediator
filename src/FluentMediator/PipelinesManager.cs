namespace FluentMediator
{
    public class PipelinesManager
    {
        public PipelineCollection<IPipeline> PipelineCollection { get; }
        public PipelineCollection<IAsyncPipeline> AsyncPipelineCollection { get; }
        public PipelineCollection<ICancellablePipeline> CancellablePipelineCollection { get; }

        public PipelinesManager()
        {
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