namespace FluentMediator.Pipelines
{
    /// <summary>
    /// Occurs when a pipeline for a message was not found
    /// </summary>
    public sealed class PipelineNotFoundException : MediatorException
    {
        /// <summary>
        /// Instantiate a PipelineNotFoundException
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>PipelineNotFoundException</returns>
        public PipelineNotFoundException(string message) : base(message) { }
    }
}