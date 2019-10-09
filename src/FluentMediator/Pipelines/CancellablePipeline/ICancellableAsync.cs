using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public interface ICancellableAsync
    {
        Task<Response> SendAsync<Response>(GetService getService, object request, CancellationToken cancellationToken);
    }
}