using System;
using System.Collections.Generic;

namespace FluentMediator.Pipelines
{
    internal sealed class PipelineCollection<TPipeline> : IPipelineCollection<TPipeline>
        where TPipeline : class, ITypedPipeline, INamedPipeline
        {
            private readonly IDictionary<Type, TPipeline> _typedPipelines;
            private readonly IDictionary<string, TPipeline> _namedPipelines;

            public PipelineCollection()
            {
                _typedPipelines = new Dictionary<Type, TPipeline>();
                _namedPipelines = new Dictionary<string, TPipeline>();
            }

            public void Add(TPipeline pipeline)
            {
                if (pipeline.Name is string)
                {
                    _namedPipelines.Add(pipeline.Name, pipeline);
                }
                else
                {
                    if (_typedPipelines.ContainsKey(pipeline.RequestType))
                    {
                        throw new PipelineAlreadyExistsException($"A pipeline for `{ pipeline.RequestType }` already exists.");
                    }
                    _typedPipelines.Add(pipeline.RequestType, pipeline);
                }
            }

            public TPipeline Get(Type requestType)
            {
                if (_typedPipelines.TryGetValue(requestType, out var pipeline))
                {
                    return pipeline;
                }

                throw new PipelineNotFoundException($"There is no pipeline configured for `{ requestType.GetType() }`.");
            }

            public TPipeline Get(string pipelineName)
            {
                if (_namedPipelines.TryGetValue(pipelineName, out var pipeline))
                {
                    return pipeline;
                }

                throw new PipelineNotFoundException($"There is no pipeline configured for `{ pipelineName }`.");
            }
        }
}