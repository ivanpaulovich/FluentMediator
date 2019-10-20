using System;

namespace FluentMediator.Pipelines
{
    public interface IPipelineCollection<TPipeline>
        where TPipeline : class, ITypedPipeline
        {
            void Add(TPipeline pipeline);
            TPipeline Get(Type requestType);
            TPipeline Get(string pipelineName);
        }
}