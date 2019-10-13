using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public sealed class PipelineBuilder<TRequest> : IPipelineBuilder<TRequest>
    {
        private readonly IMediatorBuilder _mediatorBuilder;

        public PipelineBuilder(IMediatorBuilder mediatorBuilder)
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