using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    public interface IMediator
    {
        Task PublishAsync(object request, string? pipelineName = null);
        Task<TResult> SendAsync<TResult>(object request, string? pipelineName = null);
    }
}