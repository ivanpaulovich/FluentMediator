using System;

namespace FluentMediator
{
    public partial class Mediator : ISendMediator
    {
        private PipelineCollection<ISendPipeline> _sendPipelineCollection;
        public IMediator SendPipeline<Request, Response, Handler>(Func<Handler, Request, Response> action)
        {
            var _sendPipeline = new SendPipeline<Request, Response, Handler>(this, action);
            _sendPipelineCollection.Add<Request>(_sendPipeline);
            return this;
        }

        public Response Send<Request, Response>(Request request)
        {
            if (_sendPipelineCollection.Contains<Request>(out var pipeline))
            {
                return (Response) pipeline?.Send(request!) !;
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}