namespace FluentMediator.Pipelines
{
    public sealed class PipelineAlreadyExistsException : MediatorException
    {
        public PipelineAlreadyExistsException() { }
        public PipelineAlreadyExistsException(string message) : base(message) { }
        public PipelineAlreadyExistsException(string message, System.Exception inner) : base(message, inner) { }
    }
}