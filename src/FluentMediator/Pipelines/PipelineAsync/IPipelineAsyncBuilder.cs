using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    public interface IPipelineAsyncBuilder<TRequest> : IPipelineAsyncBuilder
    {
        IPipelineAsyncBuilder<TRequest> Call<THandler>(Func<THandler, TRequest, Task> func);
        IPipelineAsync Return<TResult, THandler>(Func<THandler, TRequest, Task<TResult>> func);
    }

    public interface IPipelineAsyncBuilder
    {
        IPipelineAsync Build();
    }
}