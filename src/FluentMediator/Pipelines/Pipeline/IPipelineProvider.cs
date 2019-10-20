using System;

namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipelineProvider
    {
        IPipeline GetPipeline(Type requestType);
        IPipeline GetPipeline(string pipelineName);
    }
}