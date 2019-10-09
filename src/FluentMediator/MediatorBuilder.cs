using System;

namespace FluentMediator
{
    public class MediatorBuilder
    {
        private PipelineCollection<IPipeline> PipelineCollection { get; }
        private PipelineCollection<IAsyncPipeline> AsyncPipelineCollection { get; }
        private PipelineCollection<ICancellablePipeline> CancellablePipelineCollection { get; }

        public MediatorBuilder()
        {
            PipelineCollection = new PipelineCollection<IPipeline>();
            AsyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            CancellablePipelineCollection = new PipelineCollection<ICancellablePipeline>();
        }

        public PipelineBuilder<Request> On<Request>()
        {
            return new PipelineBuilder<Request>(this);
        }

        public Pipeline<Request> AddPipeline<Request>(Pipeline<Request> pipeline)
        {
            PipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public AsyncPipeline<Request> AddAsyncPipeline<Request>(AsyncPipeline<Request> asyncPipeline)
        {
            AsyncPipelineCollection.Add<Request>(asyncPipeline);
            return asyncPipeline;
        }

        public CancellablePipeline<Request> AddCancellablePipeline<Request>(CancellablePipeline<Request> cancellablePipeline)
        {
            CancellablePipelineCollection.Add<Request>(cancellablePipeline);
            return cancellablePipeline;
        }

        public IPipeline GetPipeline(object request)
        {
            if (PipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return pipeline!;
            }

            throw new Exception("IPipeline not found");
        }

        public IAsyncPipeline GetAsyncPipeline(object request)
        {
            if (AsyncPipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return pipeline!;
            }

            throw new Exception("IAsyncPipeline not found");
        }

        public ICancellablePipeline GetCancellablePipeline(object request)
        {
            if (CancellablePipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return pipeline!;
            }

            throw new Exception("ICancellablePipeline not found");
        }
    }
}