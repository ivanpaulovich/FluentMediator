using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.PingPong
{
    public interface IPingHandler
    {
        PingResponse MyMethod(PingRequest request);
        PingResponse MyLongMethod(PingRequest request);
        Task<PingResponse> MyMethodAsync(PingRequest request);
        Task<PingResponse> MyMethodAsync(PingRequest request, CancellationToken cancelationToken);
    }
}