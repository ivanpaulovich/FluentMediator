using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IAsyncMediator
    {
        AsyncPipeline<Request> AsyncPipeline<Request>();
        Task PublishAsync<Request>(Request request);
    }
}