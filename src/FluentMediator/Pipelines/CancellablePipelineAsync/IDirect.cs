using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    internal interface IDirect
    {
        Task<TResult> SendAsync<TResult>(
            GetService getService,
            object request,
            CancellationToken cancellationToken
        );
    }
}