namespace FluentMediator.Pipelines
{
    public class PipelineNotFoundException : MediatorException
    {
        public PipelineNotFoundException() { }
        public PipelineNotFoundException(string message) : base(message) { }
        public PipelineNotFoundException(string message, System.Exception inner) : base(message, inner) { }
    }
}