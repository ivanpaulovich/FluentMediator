using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.PingPong
{
    public class PingHandler : IPingHandler
    {
        public PingResponse MyLongMethod(PingRequest request)
        {
            return new PingResponse("Pong");
        }

        public PingResponse MyMethod(PingRequest request)
        {
            return new PingResponse("Pong");
        }

        public Task<PingResponse> MyMethodAsync(PingRequest request)
        {
            return Task.FromResult(new PingResponse("Pong"));
        }

        public Task<PingResponse> MyMethodAsync(PingRequest request, CancellationToken cancelationToken)
        {
            return Task.FromResult(new PingResponse("Pong"));
        }
    }
}