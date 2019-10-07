using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : IAsyncPipelineMediator
    {
        public PipelineCollection<IAsyncPipeline> AsyncPipelineCollection { get; }

        public async Task PublishAsync<Request>(Request request)
        {
            if (AsyncPipelineCollection.Contains<Request>(out var asyncPipeline))
            {
                await asyncPipeline?.PublishAsync(request!) !;
            }
        }
    }
}