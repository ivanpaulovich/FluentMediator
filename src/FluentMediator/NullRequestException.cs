namespace FluentMediator
{
    public class NullRequestException: MediatorException
    {
        public NullRequestException() { }
        public NullRequestException(string message) : base(message) { }
        public NullRequestException(string message, System.Exception inner) : base(message, inner) { }
    }
}