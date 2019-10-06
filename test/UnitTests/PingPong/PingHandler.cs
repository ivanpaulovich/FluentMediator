using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.PingPong
{
    public interface IPingHandler
    {
        void MyMethod(PingRequest request);
        void MyLongMethod(PingRequest request);
        Task MyMethodAsync(PingRequest request);
        Task MyMethodAsync(PingRequest request, CancellationToken cancelationToken);
    }
}