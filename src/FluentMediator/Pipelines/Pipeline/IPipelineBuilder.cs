using System;

namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipelineBuilder<TRequest>
    {
        IPipelineBuilder<TRequest> Call<THandler>(Action<THandler, TRequest> action);
        IMediatorBuilder Return<TResult, THandler>(Func<THandler, TRequest, TResult> func);
        IMediatorBuilder Build();
    }
}