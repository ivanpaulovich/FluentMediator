using System.Threading.Tasks;

namespace FluentMediator.Pipelines.PipelineAsync
{
    internal interface IDirect
    {
        Task<TResult> SendAsync<TResult>(GetService getService, object request);
    }
}