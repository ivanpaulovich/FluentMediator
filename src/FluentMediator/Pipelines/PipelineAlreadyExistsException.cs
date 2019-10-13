namespace FluentMediator.Pipelines
{
    public class PipelineAlreadyExistsException : MediatorException
    {
        public PipelineAlreadyExistsException() { }
        public PipelineAlreadyExistsException(string message) : base(message) { }
        public PipelineAlreadyExistsException(string message, System.Exception inner) : base(message, inner) { }
    }
}