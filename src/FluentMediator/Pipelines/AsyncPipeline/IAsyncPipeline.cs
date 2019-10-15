using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IAsyncPipeline
    {
        Type RequestType { get; }
        Task PublishAsync(GetService getService, object request);
        Task<TResult> SendAsync<TResult>(GetService getService, object request);
    }
}