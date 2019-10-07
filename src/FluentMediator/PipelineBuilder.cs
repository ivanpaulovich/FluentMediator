namespace FluentMediator
{
    public class PipelineBuilder<Request>
    {
        private readonly IMediator _mediator;

        public PipelineBuilder(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Pipeline<Request> BuildPipeline()
        {
            var pipeline = new Pipeline<Request>(_mediator);
            _mediator.PipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<Request> BuildAsyncPipeline()
        {
            return new AsyncPipeline<Request>(_mediator);
        }
    }
}