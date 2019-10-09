using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipeline
{
    public interface ICancellablePipelineMediator
    {
        Task PublishAsync(object request, CancellationToken ct);
    }
}