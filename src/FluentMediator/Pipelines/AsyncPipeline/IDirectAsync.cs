using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IDirectAsync
    {
        Task<TResult> SendAsync<TResult>(GetService getService, object request);
    }
}