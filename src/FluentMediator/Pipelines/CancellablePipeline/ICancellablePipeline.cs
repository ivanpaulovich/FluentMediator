using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public interface ICancellablePipeline
    {
        Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken);
        Task<Response> SendAsync<Response>(GetService getService, object request, CancellationToken cancellationToken);
        MediatorBuilder Build();
    }
}