using System.Threading.Tasks;

namespace FluentMediator
{
    public interface IDirectAsync
    {
        Task<Response> SendAsync<Request, Response>(Request request);
    }
}