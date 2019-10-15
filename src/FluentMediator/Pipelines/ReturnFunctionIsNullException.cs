namespace FluentMediator.Pipelines
{
    public sealed class ReturnFunctionIsNullException : MediatorException
    {
        public ReturnFunctionIsNullException() { }
        public ReturnFunctionIsNullException(string message) : base(message) { }
        public ReturnFunctionIsNullException(string message, System.Exception inner) : base(message, inner) { }
    }
}