using System;
using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ISendAsyncMediator
    {
        IMediator SendAsyncPipeline<Request, Response, Handler>(Func<Handler, Request, Task<Response>> action);
        Task<Response> SendAsync<Request, Response>(Request request);
    }
}