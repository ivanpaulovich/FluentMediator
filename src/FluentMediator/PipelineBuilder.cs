namespace FluentMediator
{
    public class PipelineBuilder<Request>
    {
        private readonly IMediator _mediator;

        public PipelineBuilder(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Pipeline<Request> Pipeline()
        {
            var pipeline = new Pipeline<Request>(_mediator);
            _mediator.PipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<Request> AsyncPipeline()
        {
            var asyncPipeline = new AsyncPipeline<Request>(_mediator);
            _mediator.AsyncPipelineCollection.Add<Request>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellablePipeline<Request> CancellablePipeline()
        {
            var cancellableAsyncPipeline = new CancellablePipeline<Request>(_mediator);
            _mediator.CancellablePipelineCollection.Add<Request>(cancellableAsyncPipeline);
            return cancellableAsyncPipeline;
        }
    }
}