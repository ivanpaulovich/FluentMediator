using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : IAsyncPipelineMediator
    {
        public async Task PublishAsync<Request>(Request request)
        {
            if (PipelinesManager.AsyncPipelineCollection.Contains<Request>(out var asyncPipeline))
            {
                await asyncPipeline?.PublishAsync(GetService, request!) !;
            }
        }

        public async Task<Response> SendAsync<Response>(object request)
        {
            if (PipelinesManager.AsyncPipelineCollection.Contains(request.GetType(), out var asyncPipeline))
            {
                if (!(asyncPipeline is null))
                {
                    return await asyncPipeline.SendAsync<Response>(GetService, request!) !;
                }
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}