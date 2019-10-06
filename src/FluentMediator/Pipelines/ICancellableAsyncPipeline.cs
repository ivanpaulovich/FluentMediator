using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.CancellableAsyncPipeline
{
    public interface ICancellableAsyncPipeline
    {
        Task PublishAsync(object request, CancellationToken cancellationToken);
    }
}