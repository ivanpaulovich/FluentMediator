using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator
    {
        private readonly PipelineCollection<IDirectAsync> _sendAsyncPipelineCollection;
        public IMediator DirectAsync<Request, Response, Handler>(Func<Handler, Request, Task<Response>> action)
        {
            var _sendPipeline = new DirectAsync<Request, Response, Handler>(this, action);
            _sendAsyncPipelineCollection.Add<Request>(_sendPipeline);
            return this;
        }

        public async Task<Response> SendAsync<Response>(object request)
        {
            if (_sendAsyncPipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return await pipeline?.SendAsync<Response>(request!) !;
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}