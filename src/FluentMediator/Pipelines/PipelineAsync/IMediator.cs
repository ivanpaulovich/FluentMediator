using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    public interface IMediator
    {
        Task PublishAsync(object request);
        Task<TResult> SendAsync<TResult>(object request);
    }
}