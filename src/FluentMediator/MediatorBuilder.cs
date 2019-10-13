using System;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public sealed class MediatorBuilder : IMediatorBuilder
    {
        private IPipelineCollection<IPipeline> _pipelineCollection { get; }
        private IPipelineCollection<IAsyncPipeline> _asyncPipelineCollection { get; }
        private IPipelineCollection<ICancellablePipeline> _cancellablePipelineCollection { get; }

        public MediatorBuilder()
        {
            _pipelineCollection = new PipelineCollection<IPipeline>();
            _asyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            _cancellablePipelineCollection = new PipelineCollection<ICancellablePipeline>();
        }

        public IPipelineBehavior<TRequest> On<TRequest>()
        {
            return new PipelineBuilder<TRequest>(this);
        }

        public IPipelineBuilder<TRequest> AddPipeline<TRequest>(Pipeline<TRequest> pipeline)
        {
            _pipelineCollection.Add<TRequest>(pipeline);
            return pipeline;
        }

        public IAsyncPipelineBuilder<TRequest> AddAsyncPipeline<TRequest>(AsyncPipeline<TRequest> asyncPipeline)
        {
            _asyncPipelineCollection.Add<TRequest>(asyncPipeline);
            return asyncPipeline;
        }

        public ICancellablePipelineBuilder<TRequest> AddCancellablePipeline<TRequest>(CancellablePipeline<TRequest> cancellablePipeline)
        {
            _cancellablePipelineCollection.Add<TRequest>(cancellablePipeline);
            return cancellablePipeline;
        }

        public IMediator Build(GetService getService)
        {
            return new Mediator(getService, _pipelineCollection, _asyncPipelineCollection, _cancellablePipelineCollection);
        }
    }
}