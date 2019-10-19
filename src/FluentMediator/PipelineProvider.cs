using System;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    internal sealed class PipelineProvider : IPipelineProvider
    {
        private readonly IPipelineCollection<IPipeline> _pipelineCollection;
        private readonly IPipelineCollection<IAsyncPipeline> _asyncPipelineCollection;
        private readonly IPipelineCollection<ICancellablePipeline> _cancellablePipelineCollection;

        public PipelineProvider(
            IPipelineCollection<IPipeline> pipelineCollection,
            IPipelineCollection<IAsyncPipeline> asyncPipelineCollection,
            IPipelineCollection<ICancellablePipeline> cancellablePipelineCollection)
        {
            _pipelineCollection = pipelineCollection;
            _asyncPipelineCollection = asyncPipelineCollection;
            _cancellablePipelineCollection = cancellablePipelineCollection;
        }

        public IAsyncPipeline GetAsyncPipeline(Type requestType)
        {
            return _asyncPipelineCollection.Get(requestType);
        }

        public ICancellablePipeline GetCancellablePipeline(Type requestType)
        {
            return _cancellablePipelineCollection.Get(requestType);
        }

        public IPipeline GetPipeline(Type requestType)
        {
            return _pipelineCollection.Get(requestType);
        }
    }
}