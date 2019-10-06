using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ISendAsyncPipeline
    {
        Task<Response> SendAsync<Request, Response>(Request request);
    }
}