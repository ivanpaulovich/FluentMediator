using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IAsyncPipeline
    {
        Task PublishAsync(GetService getService, object request);
        Task<Response> SendAsync<Response>(GetService getService, object request);
        PipelinesManager Build();
    }
}