using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IAsyncPipelineMediator
    {
        PipelineCollection<IAsyncPipeline> AsyncPipelineCollection { get; }
        Task PublishAsync<Request>(Request request);
    }
}