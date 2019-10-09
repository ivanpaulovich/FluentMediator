using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : IAsyncPipelineMediator
    {
        public async Task PublishAsync(object request)
        {
            var pipeline = MediatorBuilder.GetAsyncPipeline(request);
            await pipeline.PublishAsync(GetService, request!);
        }

        public async Task<Response> SendAsync<Response>(object request)
        {
            var pipeline = MediatorBuilder.GetAsyncPipeline(request);
            return await pipeline.SendAsync<Response>(GetService, request!);
        }
    }
}