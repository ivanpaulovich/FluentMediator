using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IAsyncPipelineMediator
    {
        Task PublishAsync(object request);
        Task<Response> SendAsync<Response>(object request);
    }
}