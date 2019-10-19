using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.PingPong
{
    public class PingHandler : IPingHandler
    {
        public PingResponse MyCustomBarMethod(PingRequest request)
        {
            return new PingResponse("Pong");
        }

        public PingResponse MyCustomFooMethod(PingRequest request)
        {
            return new PingResponse("Pong");
        }

        public Task<PingResponse> MyCustomFooBarAsync(PingRequest request)
        {
            return Task.FromResult(new PingResponse("Pong"));
        }

        public Task<PingResponse> MyCancellableForAsync(PingRequest request, CancellationToken cancelationToken)
        {
            return Task.FromResult(new PingResponse("Pong"));
        }
    }
}