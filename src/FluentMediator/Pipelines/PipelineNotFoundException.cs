namespace FluentMediator.Pipelines
{
    public sealed class PipelineNotFoundException : MediatorException
    {
        public PipelineNotFoundException() { }
        public PipelineNotFoundException(string message) : base(message) { }
        public PipelineNotFoundException(string message, System.Exception inner) : base(message, inner) { }
    }
}