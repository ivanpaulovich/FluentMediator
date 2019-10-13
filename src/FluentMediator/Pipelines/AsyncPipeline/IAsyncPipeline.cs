using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IAsyncPipeline
    {
        Task PublishAsync(GetService getService, object request);
        Task<TResult> SendAsync<TResult>(GetService getService, object request);
    }
}