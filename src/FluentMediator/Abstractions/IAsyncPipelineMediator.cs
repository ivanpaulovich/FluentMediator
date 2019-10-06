using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IAsyncPipelineMediator
    {
        AsyncPipeline<Request> AsyncPipeline<Request>();
        Task PublishAsync<Request>(Request request);
    }
}