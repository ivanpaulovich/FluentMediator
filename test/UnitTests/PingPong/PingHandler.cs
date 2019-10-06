using System;
using System.Diagnostics;

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
    }
}