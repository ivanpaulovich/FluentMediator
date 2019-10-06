using System;

namespace FluentMediator
{
    public interface ISendMediator
    {
        IMediator SendPipeline<Request, Response, Handler>(Func<Handler, Request, Response> action);
        Response Send<Request, Response>(Request request);
    }
}