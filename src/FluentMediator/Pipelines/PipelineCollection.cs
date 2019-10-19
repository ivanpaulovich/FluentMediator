using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FluentMediator.Pipelines
{
    internal sealed class PipelineCollection<TPipeline> : IPipelineCollection<TPipeline>
        where TPipeline : class, ITypedPipeline
        {
            private readonly IDictionary<Type, TPipeline> _pipelines;

            public PipelineCollection()
            {
                _pipelines = new Dictionary<Type, TPipeline>();
            }

            public void Add(TPipeline pipeline)
            {
                if (_pipelines.ContainsKey(pipeline.RequestType))
                {
                    throw new PipelineAlreadyExistsException($"A pipeline for `{ pipeline.RequestType }` already exists.");
                }

                _pipelines.Add(pipeline.RequestType, pipeline);
            }

            public TPipeline Get(Type requestType)
            {
                if (_pipelines.TryGetValue(requestType, out var pipeline))
                {
                    return pipeline;
                }

                throw new PipelineNotFoundException($"There is no pipeline configured for `{ requestType.GetType() }`.");
            }

            public bool Contains(Type requestType, out TPipeline? pipeline)
            {
                if (!_pipelines.ContainsKey(requestType))
                {
                    pipeline = default;
                    return false;
                }

                pipeline = _pipelines[requestType];
                return true;
            }

            public IEnumerable<TPipeline> ToIEnumerable()
            {
                return new ReadOnlyCollection<TPipeline>(_pipelines.Values.ToList());
            }
        }
}