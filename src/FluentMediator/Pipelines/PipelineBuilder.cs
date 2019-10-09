using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public class PipelineBuilder<Request> : IPipelineBuilder
    {
        private readonly MediatorBuilder _mediatorBuilder;

        public PipelineBuilder(MediatorBuilder mediatorBuilder)
        {
            _mediatorBuilder = mediatorBuilder;
        }

        public Pipeline<Request> Pipeline()
        {
            var pipeline = new Pipeline<Request>(_mediatorBuilder);
            _mediatorBuilder.AddPipeline<Request>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<Request> AsyncPipeline()
        {
            var asyncPipeline = new AsyncPipeline<Request>(_mediatorBuilder);
            _mediatorBuilder.AddAsyncPipeline<Request>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellablePipeline<Request> CancellablePipeline()
        {
            var cancellableAsyncPipeline = new CancellablePipeline<Request>(_mediatorBuilder);
            _mediatorBuilder.AddCancellablePipeline<Request>(cancellableAsyncPipeline);
            return cancellableAsyncPipeline;
        }
    }
}