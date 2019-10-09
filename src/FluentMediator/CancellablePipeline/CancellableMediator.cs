using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : ICancellablePipelineMediator
    {
        public async Task PublishAsync(object request, CancellationToken ct)
        {
            var pipeline = MediatorBuilder.GetCancellablePipeline(request);
            await pipeline.PublishAsync(GetService, request!, ct);
        }

        public async Task<Response> SendAsync<Response>(object request, CancellationToken ct)
        {
            var pipeline = MediatorBuilder.GetCancellablePipeline(request);
            return await pipeline.SendAsync<Response>(GetService, request!, ct);
        }
    }
}