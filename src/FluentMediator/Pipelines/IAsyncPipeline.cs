using System.Threading.Tasks;

namespace FluentMediator.AsyncPipeline
{
    public interface IAsyncPipeline
    {
        Task PublishAsync(object request);
    }
}