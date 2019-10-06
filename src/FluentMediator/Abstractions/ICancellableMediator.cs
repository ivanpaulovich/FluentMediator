using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ICancellableMediator
    {
        CancellablePipeline<Request> CancellablePipeline<Request>();
        Task PublishAsync<Request>(Request request, CancellationToken ct);
    }
}