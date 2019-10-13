using System;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public class MediatorBuilder
    {
        private PipelineCollection<IPipeline> _pipelineCollection { get; }
        private PipelineCollection<IAsyncPipeline> _asyncPipelineCollection { get; }
        private PipelineCollection<ICancellablePipeline> _cancellablePipelineCollection { get; }

        public MediatorBuilder()
        {
            _pipelineCollection = new PipelineCollection<IPipeline>();
            _asyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            _cancellablePipelineCollection = new PipelineCollection<ICancellablePipeline>();
        }

        public PipelineBuilder<TRequest> On<TRequest>()
        {
            return new PipelineBuilder<TRequest>(this);
        }

        public Pipeline<TRequest> AddPipeline<TRequest>(Pipeline<TRequest> pipeline)
        {
            _pipelineCollection.Add<TRequest>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<TRequest> AddAsyncPipeline<TRequest>(AsyncPipeline<TRequest> asyncPipeline)
        {
            _asyncPipelineCollection.Add<TRequest>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellablePipeline<TRequest> AddCancellablePipeline<TRequest>(CancellablePipeline<TRequest> cancellablePipeline)
        {
            _cancellablePipelineCollection.Add<TRequest>(cancellablePipeline);
            return cancellablePipeline;
        }

        public IPipeline GetPipeline(object request)
        {
            if (_pipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return pipeline!;
            }

            throw new Exception("IPipeline not found");
        }

        public IAsyncPipeline GetAsyncPipeline(object request)
        {
            if (_asyncPipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return pipeline!;
            }

            throw new Exception("IAsyncPipeline not found");
        }

        public ICancellablePipeline GetCancellablePipeline(object request)
        {
            if (_cancellablePipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return pipeline!;
            }

            throw new Exception("ICancellablePipeline not found");
        }

        public IMediator Build(GetService getService)
        {
            return new Mediator(getService, _pipelineCollection, _asyncPipelineCollection, _cancellablePipelineCollection);
        }
    }
}