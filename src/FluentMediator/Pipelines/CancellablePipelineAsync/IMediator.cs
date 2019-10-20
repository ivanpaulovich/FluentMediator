using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    public interface IMediator
    {
        Task PublishAsync(object request, CancellationToken ct);
        Task<TResult> SendAsync<TResult>(object request, CancellationToken ct);
    }
}