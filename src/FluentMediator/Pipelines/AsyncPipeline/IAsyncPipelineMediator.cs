using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IAsyncPipelineMediator
    {
        Task PublishAsync<Request>(Request request);
        Task<Response> SendAsync<Response>(object request);
    }
}