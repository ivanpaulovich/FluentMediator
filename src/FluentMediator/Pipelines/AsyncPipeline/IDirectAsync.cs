using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    public interface IDirectAsync
    {
        Task<Response> SendAsync<Response>(GetService getService, object request);
    }
}