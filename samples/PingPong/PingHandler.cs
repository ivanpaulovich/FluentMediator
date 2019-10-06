using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.PingPong
{
    public class PingHandler
    {
        public void MyMethod(PingRequest request)
        {
            Debug.WriteLine($"{ request.Message } .. Pong ");
        }

        public void MyLongMethod(PingRequest request)
        {
            Debug.WriteLine($"{ request.Message } ............... Pong");
        }

        public async Task MyMethodAsync(PingRequest request)
        {
            Debug.WriteLine($"{ request.Message } > > > > Pong ");
            await Task.CompletedTask;
        }

        public async Task MyMethodAsync(PingRequest request, CancellationToken cancelationToken)
        {
            Debug.WriteLine($"{ request.Message } - - - - - Pong ");
            await Task.CompletedTask;
        }
    }
}