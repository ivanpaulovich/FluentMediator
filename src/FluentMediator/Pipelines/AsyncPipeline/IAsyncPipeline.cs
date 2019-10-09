using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IAsyncPipeline
    {
        Task PublishAsync(GetService getService, object request);
        Task<Response> SendAsync<Response>(GetService getService, object request);
        MediatorBuilder Build();
    }
}