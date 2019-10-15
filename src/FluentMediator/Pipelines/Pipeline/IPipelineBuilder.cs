using System;

namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipelineBuilder<TRequest> : IPipelineBuilder
    {
        IPipelineBuilder<TRequest> Call<THandler>(Action<THandler, TRequest> action);
        IPipeline Return<TResult, THandler>(Func<THandler, TRequest, TResult> func);
    }

    public interface IPipelineBuilder
    {
        IPipeline Build();
    }
}