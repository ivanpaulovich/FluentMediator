using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator
{
    public interface ICancellableAsync
    {
        Task<Response> SendAsync<Response>(object request, CancellationToken cancellationToken);
    }
}