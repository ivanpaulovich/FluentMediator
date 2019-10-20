using System;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator
{
    internal sealed class PipelineProvider : IPipelineProvider
    {
        private readonly IPipelineCollection<IPipeline> _pipelineCollection;
        private readonly IPipelineCollection<IPipelineAsync> _asyncPipelineCollection;
        private readonly IPipelineCollection<ICancellablePipelineAsync> _cancellablePipelineCollection;

        public PipelineProvider(
            IPipelineCollection<IPipeline> pipelineCollection,
            IPipelineCollection<IPipelineAsync> asyncPipelineCollection,
            IPipelineCollection<ICancellablePipelineAsync> cancellablePipelineCollection)
        {
            _pipelineCollection = pipelineCollection;
            _asyncPipelineCollection = asyncPipelineCollection;
            _cancellablePipelineCollection = cancellablePipelineCollection;
        }

        public IPipelineAsync GetAsyncPipeline(Type requestType)
        {
            return _asyncPipelineCollection.Get(requestType);
        }

        public ICancellablePipelineAsync GetCancellablePipeline(Type requestType)
        {
            return _cancellablePipelineCollection.Get(requestType);
        }

        public IPipeline GetPipeline(Type requestType)
        {
            return _pipelineCollection.Get(requestType);
        }
    }
}