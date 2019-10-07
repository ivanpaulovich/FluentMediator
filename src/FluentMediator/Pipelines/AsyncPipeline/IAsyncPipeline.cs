using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IAsyncPipeline
    {
        Task PublishAsync(object request);
        Task<Response> SendAsync<Response>(object request);
        IMediator Build();
    }
}