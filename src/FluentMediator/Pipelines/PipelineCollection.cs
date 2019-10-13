using System;
using System.Collections.Generic;

namespace FluentMediator.Pipelines
{
    public sealed class PipelineCollection<TPipeline> : IPipelineCollection<TPipeline>
        where TPipeline : class
    {
        private readonly IDictionary<Type, TPipeline> _pipelines;

        public PipelineCollection()
        {
            _pipelines = new Dictionary<Type, TPipeline>();
        }

        public void Add<TRequest>(TPipeline pipeline)
        {
            if (_pipelines.ContainsKey(typeof(TRequest)))
            {
                throw new PipelineAlreadyExistsException($"A pipeline for `{ typeof(TRequest) }` already exists.");
            }

            _pipelines.Add(typeof(TRequest), pipeline);
        }

        public TPipeline Get(object request)
        {
            if (_pipelines.TryGetValue(request.GetType(), out var pipeline))
            {
                return pipeline;
            }
            
            throw new PipelineNotFoundException($"There is no pipeline configured for `{ request.GetType() }`.");
        }

        public bool Contains(Type requestType, out TPipeline? pipeline)
        {
            if (!_pipelines.ContainsKey(requestType))
            {
                pipeline = default(TPipeline);
                return false;
            }

            pipeline = _pipelines[requestType];
            return true;
        }
    }
}