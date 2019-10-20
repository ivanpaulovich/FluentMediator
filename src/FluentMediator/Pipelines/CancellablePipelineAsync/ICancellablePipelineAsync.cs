using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    public interface ICancellablePipelineAsync : INamedPipeline, ITypedPipeline
    {
        Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken);
        Task<TResult> SendAsync<TResult>(GetService getService, object request, CancellationToken cancellationToken);
    }
}