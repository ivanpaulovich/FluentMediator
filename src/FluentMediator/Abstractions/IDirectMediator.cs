using System;

namespace FluentMediator
{
    public interface IDirectMediator
    {
        IMediator Direct<Request, Response, Handler>(Func<Handler, Request, Response> action);
        Response Send<Request, Response>(Request request);
    }
}