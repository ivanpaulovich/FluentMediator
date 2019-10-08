using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ICancellablePipelineMediator
    {
        Task PublishAsync<Request>(Request request, CancellationToken ct);
    }
}