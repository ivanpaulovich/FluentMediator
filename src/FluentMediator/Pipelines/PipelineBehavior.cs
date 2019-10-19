using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator.Pipelines
{
    internal sealed class PipelineBehavior<TRequest> : IPipelineBehavior<TRequest>
    {
        private readonly IPipelineProviderBuilder _mediatorBuilder;

        public PipelineBehavior(IPipelineProviderBuilder mediatorBuilder)
        {
            _mediatorBuilder = mediatorBuilder;
        }

        public IPipelineBuilder<TRequest> Pipeline(string? name = null)
        {
            var pipelineBuilder = new PipelineBuilder<TRequest>(name);
            _mediatorBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        public IAsyncPipelineBuilder<TRequest> AsyncPipeline(string? name = null)
        {
            var pipelineBuilder = new AsyncPipelineBuilder<TRequest>(name);
            _mediatorBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        public ICancellablePipelineBuilder<TRequest> CancellablePipeline(string? name = null)
        {
            var pipelineBuilder = new CancellablePipelineBuilder<TRequest>(name);
            _mediatorBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }
    }
}