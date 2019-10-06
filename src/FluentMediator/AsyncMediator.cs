using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : IAsyncPipelineMediator
    {
        private readonly PipelineCollection<IAsyncPipeline> _asyncPipelineCollection;

        public AsyncPipeline<Request> AsyncPipeline<Request>()
        {
            var asyncPipeline = new AsyncPipeline<Request>(this);
            _asyncPipelineCollection.Add<Request>(asyncPipeline);
            return asyncPipeline;
        }

        public async Task PublishAsync<Request>(Request request)
        {
            if (_asyncPipelineCollection.Contains<Request>(out var asyncPipeline))
            {
                await asyncPipeline?.PublishAsync(request!) !;
            }
        }
    }
}