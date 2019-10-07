using System;
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

        public async Task<Response> SendAsync<Response>(object request, CancellationToken ct)
        {
            if (CancellablePipelineCollection.Contains(request.GetType(), out var cancellableAsyncPipeline))
            {
                if (!(cancellableAsyncPipeline is null))
                {
                    return await cancellableAsyncPipeline.SendAsync<Response>(request!, ct) !;
                }
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}