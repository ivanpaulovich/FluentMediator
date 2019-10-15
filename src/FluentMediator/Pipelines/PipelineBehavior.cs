using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    public sealed class PipelineBehavior<TRequest> : IPipelineBehavior<TRequest>
    {
        private readonly IPipelinesBuilder _mediatorBuilder;

        public PipelineBehavior(IPipelinesBuilder mediatorBuilder)
        {
            _mediatorBuilder = mediatorBuilder;
        }

        public IPipelineBuilder<TRequest> Pipeline()
        {
            var pipelineBuilder = new PipelineBuilder<TRequest>();
            _mediatorBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        public IAsyncPipelineBuilder<TRequest> AsyncPipeline()
        {
            var pipelineBuilder = new AsyncPipelineBuilder<TRequest>();
            _mediatorBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        public ICancellablePipelineBuilder<TRequest> CancellablePipeline()
        {
            var pipelineBuilder = new CancellablePipelineBuilder<TRequest>();
            _mediatorBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }
    }
}