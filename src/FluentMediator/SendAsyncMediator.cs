using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public partial class Mediator : ISendMediator
    {
        private PipelineCollection<ISendAsyncPipeline> _sendAsyncPipelineCollection;
        public IMediator SendAsyncPipeline<Request, Response, Handler>(Func<Handler, Request, Task<Response>> action)
        {
            var _sendPipeline = new SendAsyncPipeline<Request, Response, Handler>(this, action);
            _sendAsyncPipelineCollection.Add<Request>(_sendPipeline);
            return this;
        }

        public async Task<Response> SendAsync<Request, Response>(Request request)
        {
            if (_sendAsyncPipelineCollection.Contains<Request>(out var pipeline))
            {
                return await pipeline.SendAsync<Request, Response>(request!);
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}