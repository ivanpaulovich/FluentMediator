namespace FluentMediator
{
    public class PipelineBuilder<Request>
    {
        private readonly PipelinesManager _pipelinesManager;

        public PipelineBuilder(PipelinesManager pipelinesManager)
        {
            _pipelinesManager = pipelinesManager;
        }

        public Pipeline<Request> Pipeline()
        {
            var pipeline = new Pipeline<Request>(_pipelinesManager);
            _pipelinesManager.PipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<Request> AsyncPipeline()
        {
            var asyncPipeline = new AsyncPipeline<Request>(_pipelinesManager);
            _pipelinesManager.AsyncPipelineCollection.Add<Request>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellablePipeline<Request> CancellablePipeline()
        {
            var cancellableAsyncPipeline = new CancellablePipeline<Request>(_pipelinesManager);
            _pipelinesManager.CancellablePipelineCollection.Add<Request>(cancellableAsyncPipeline);
            return cancellableAsyncPipeline;
        }
    }
}