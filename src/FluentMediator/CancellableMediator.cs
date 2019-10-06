using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : ICancellableMediator
    {
        private PipelineCollection<ICancellablePipeline> _cancellablePipelineCollection;

        public CancellablePipeline<Request> CancellablePipeline<Request>()
        {
            var cancellableAsyncPipeline = new CancellablePipeline<Request>(this);
            _cancellablePipelineCollection.Add<Request>(cancellableAsyncPipeline);
            return cancellableAsyncPipeline;
        }

        public async Task PublishAsync<Request>(Request request, CancellationToken ct)
        {
            if (_cancellablePipelineCollection.Contains<Request>(out var cancellableAsyncPipeline))
            {
                await cancellableAsyncPipeline.PublishAsync(request!, ct);
            }
        }
    }
}