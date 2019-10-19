using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    public interface IPipelineAsync : INamedPipeline, ITypedPipeline
    {
        Task PublishAsync(GetService getService, object request);
        Task<TResult> SendAsync<TResult>(GetService getService, object request);
    }
}