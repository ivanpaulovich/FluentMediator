using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IAsyncPipeline
    {
        Task PublishAsync(object request);
    }
}