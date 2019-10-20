using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingPong
{
    public class PingHandler
    {
        public void MyMethod(PingRequest request)
        {
            Console.WriteLine($"{ request.Message } .. Pong ");
        }

        public void MyLongMethod(PingRequest request)
        {
            Console.WriteLine($"{ request.Message } ............... Pong");
        }

        public async Task MyMethodAsync(PingRequest request)
        {
            Console.WriteLine($"{ request.Message } > > > > Pong ");
            await Task.CompletedTask;
        }

        public async Task MyMethodAsync(PingRequest request, CancellationToken cancelationToken)
        {
            Console.WriteLine($"{ request.Message } - - - - - Pong ");
            await Task.CompletedTask;
        }
    }
}