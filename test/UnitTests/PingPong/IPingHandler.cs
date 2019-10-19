using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.PingPong
{
    public interface IPingHandler
    {
        PingResponse MyCustomFooMethod(PingRequest request);
        PingResponse MyCustomBarMethod(PingRequest request);
        Task<PingResponse> MyCustomFooBarAsync(PingRequest request);
        Task<PingResponse> MyCancellableForAsync(PingRequest request, CancellationToken cancelationToken);
    }
}