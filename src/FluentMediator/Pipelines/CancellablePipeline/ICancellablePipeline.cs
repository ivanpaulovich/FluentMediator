using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public interface ICancellablePipeline
    {
        Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken);
        Task<TResult> SendAsync<TResult>(GetService getService, object request, CancellationToken cancellationToken);
        MediatorBuilder Build();
    }
}