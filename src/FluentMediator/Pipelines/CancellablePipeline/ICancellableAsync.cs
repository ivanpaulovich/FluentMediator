using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public interface ICancellableAsync
    {
        Task<TResult> SendAsync<TResult>(GetService getService, object request, CancellationToken cancellationToken);
    }
}