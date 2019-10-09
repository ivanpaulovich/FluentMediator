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

        public PipelineBuilder<Request> On<Request>()
        {
            return new PipelineBuilder<Request>(this);
        }

        public Pipeline<Request> AddPipeline<Request>(Pipeline<Request> pipeline)
        {
            _pipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<Request> AddAsyncPipeline<Request>(AsyncPipeline<Request> asyncPipeline)
        {
            _asyncPipelineCollection.Add<Request>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellablePipeline<Request> AddCancellablePipeline<Request>(CancellablePipeline<Request> cancellablePipeline)
        {
            _cancellablePipelineCollection.Add<Request>(cancellablePipeline);
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