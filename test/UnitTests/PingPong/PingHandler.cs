using System;

namespace UnitTests.PingPong
{
    public class PingHandler
    {
        public void MyHandler(PingRequest request)
        {
            Console.WriteLine($"MyHandler { request.Message }");
        }

        public void LongHandler(PingRequest request)
        {
            Console.WriteLine($"LongHandler { request.Message }");
        }
    }
}