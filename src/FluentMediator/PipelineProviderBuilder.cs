using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public sealed class PipelineProviderBuilder : IPipelineProviderBuilder
    {
        private ICollection<IPipelineBuilder> _pipelineBuilderCollection { get; }
        private ICollection<IAsyncPipelineBuilder> _asyncPipelineBuilderCollection { get; }
        private ICollection<ICancellablePipelineBuilder> _cancellablePipelineBuilderCollection { get; }

        public PipelineProviderBuilder()
        {
            _pipelineBuilderCollection = new Collection<IPipelineBuilder>();
            _asyncPipelineBuilderCollection = new Collection<IAsyncPipelineBuilder>();
            _cancellablePipelineBuilderCollection = new Collection<ICancellablePipelineBuilder>();
        }

        public IPipelineBehavior<TRequest> On<TRequest>()
        {
            var behavior = new PipelineBehavior<TRequest>(this);
            return behavior;
        }

        public IPipelineBuilder Add(IPipelineBuilder pipeline)
        {
            _pipelineBuilderCollection.Add(pipeline);
            return pipeline;
        }

        public IAsyncPipelineBuilder Add(IAsyncPipelineBuilder asyncPipeline)
        {
            _asyncPipelineBuilderCollection.Add(asyncPipeline);
            return asyncPipeline;
        }

        public ICancellablePipelineBuilder Add(ICancellablePipelineBuilder cancellablePipeline)
        {
            _cancellablePipelineBuilderCollection.Add(cancellablePipeline);
            return cancellablePipeline;
        }

        public IPipelineProvider Build()
        {
            var pipelineCollection = new PipelineCollection<IPipeline>();
            var asyncPipelineCollection = new PipelineCollection<IAsyncPipeline>();
            var cancellablePipelineCollection = new PipelineCollection<ICancellablePipeline>();

            foreach (var item in _pipelineBuilderCollection)
            {
                var pipeline = item.Build();
                pipelineCollection.Add(pipeline.RequestType, pipeline);
            }

            foreach (var item in _asyncPipelineBuilderCollection)
            {
                var pipeline = item.Build();
                asyncPipelineCollection.Add(pipeline.RequestType, pipeline);
            }

            foreach (var item in _cancellablePipelineBuilderCollection)
            {
                var pipeline = item.Build();
                cancellablePipelineCollection.Add(pipeline.RequestType, pipeline);
            }

            return new PipelineProvider(
                pipelineCollection,
                asyncPipelineCollection,
                cancellablePipelineCollection
            );
        }
    }
}