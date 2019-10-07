using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ICancellablePipeline
    {
        Task PublishAsync(object request, CancellationToken cancellationToken);
        IMediator Build();
    }
}