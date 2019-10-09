using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ICancellablePipelineMediator
    {
        Task PublishAsync(object request, CancellationToken ct);
    }
}