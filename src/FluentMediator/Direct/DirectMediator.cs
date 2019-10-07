using System;

namespace FluentMediator
{
    public partial class Mediator
    {
        private readonly PipelineCollection<IDirect> _sendPipelineCollection;
        public IMediator Direct<Request, Response, Handler>(Func<Handler, Request, Response> action)
        {
            var _sendPipeline = new Direct<Request, Response, Handler>(this, action);
            _sendPipelineCollection.Add<Request>(_sendPipeline);
            return this;
        }

        public Response Send<Response>(object request)
        {
            if (_sendPipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                return (Response) pipeline?.Send(request!) !;
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}