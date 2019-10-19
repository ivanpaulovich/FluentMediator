using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator.Pipelines
{
    internal sealed class PipelineBehavior<TRequest> : IPipelineBehavior<TRequest>
    {
        private readonly IPipelineProviderBuilder _pipelineProviderBuilder;

        public PipelineBehavior(IPipelineProviderBuilder pipelineProviderBuilder)
        {
            _pipelineProviderBuilder = pipelineProviderBuilder;
        }

        public IPipelineBuilder<TRequest> Pipeline(string? pipelineName = null)
        {
            var pipelineBuilder = new Pipeline.PipelineBuilder<TRequest>(pipelineName);
            _pipelineProviderBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        public IPipelineAsyncBuilder<TRequest> PipelineAsync(string? pipelineName = null)
        {
            var pipelineBuilder = new PipelineAsync.PipelineBuilder<TRequest>(pipelineName);
            _pipelineProviderBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        public ICancellablePipelineAsyncBuilder<TRequest> CancellablePipelineAsync(string? pipelineName = null)
        {
            var pipelineBuilder = new CancellablePipelineAsync.PipelineBuilder<TRequest>(pipelineName);
            _pipelineProviderBuilder.Add(pipelineBuilder);
            return pipelineBuilder;
        }
    }
}