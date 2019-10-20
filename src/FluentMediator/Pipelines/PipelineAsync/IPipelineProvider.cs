using System;

namespace FluentMediator.Pipelines.PipelineAsync
{
    public interface IPipelineProvider
    {
        IPipelineAsync GetAsyncPipeline(Type requestType);
        IPipelineAsync GetAsyncPipeline(string pipelineName);
    }
}