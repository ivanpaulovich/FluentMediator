namespace UnitTests.PingPong
{
    public class PingResponse
    {
        public string Message { get; }

        public PingResponse(string message)
        {
            Message = message;
        }
    }
}