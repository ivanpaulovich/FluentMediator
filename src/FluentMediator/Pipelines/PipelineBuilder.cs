using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public class PipelineBuilder<TRequest> : IPipelineBuilder
    {
        private readonly MediatorBuilder _mediatorBuilder;

        public PipelineBuilder(MediatorBuilder mediatorBuilder)
        {
            _mediatorBuilder = mediatorBuilder;
        }

        public Pipeline<TRequest> Pipeline()
        {
            var pipeline = new Pipeline<TRequest>(_mediatorBuilder);
            _mediatorBuilder.AddPipeline<TRequest>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<TRequest> AsyncPipeline()
        {
            var asyncPipeline = new AsyncPipeline<TRequest>(_mediatorBuilder);
            _mediatorBuilder.AddAsyncPipeline<TRequest>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellablePipeline<TRequest> CancellablePipeline()
        {
            var cancellableAsyncPipeline = new CancellablePipeline<TRequest>(_mediatorBuilder);
            _mediatorBuilder.AddCancellablePipeline<TRequest>(cancellableAsyncPipeline);
            return cancellableAsyncPipeline;
        }
    }
}