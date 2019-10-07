using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ICancellablePipelineMediator
    {
        PipelineCollection<ICancellablePipeline> CancellablePipelineCollection { get; }
        Task PublishAsync<Request>(Request request, CancellationToken ct);
    }
}