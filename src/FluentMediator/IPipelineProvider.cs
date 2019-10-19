using System;
using FluentMediator.Pipelines.AsyncPipeline;
using FluentMediator.Pipelines.CancellablePipeline;
using FluentMediator.Pipelines.Pipeline;

namespace FluentMediator
{
    public interface IPipelineProvider
    {
        IPipeline GetPipeline(Type requestType);
        IAsyncPipeline GetAsyncPipeline(Type requestType);
        ICancellablePipeline GetCancellablePipeline(Type requestType);
    }
}