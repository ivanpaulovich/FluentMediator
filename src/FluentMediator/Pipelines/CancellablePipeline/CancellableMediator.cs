using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : ICancellablePipelineMediator
    {
        public PipelineCollection<ICancellablePipeline> CancellablePipelineCollection { get; }

        public async Task PublishAsync<Request>(Request request, CancellationToken ct)
        {
            if (CancellablePipelineCollection.Contains<Request>(out var cancellableAsyncPipeline))
            {
                await cancellableAsyncPipeline?.PublishAsync(request!, ct) !;
            }
        }
    }
}