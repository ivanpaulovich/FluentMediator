using System;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    public interface IPipelineProvider
    {
        ICancellablePipelineAsync GetCancellablePipeline(Type requestType);
    }
}