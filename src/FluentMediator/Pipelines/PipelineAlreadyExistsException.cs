namespace FluentMediator.Pipelines
{
    /// <summary>
    /// Occurs when pipelines have the same name
    /// </summary>
    public sealed class PipelineAlreadyExistsException : MediatorException
    {
        /// <summary>
        /// Instantiate an PipelineAlreadyExistsException
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>PipelineAlreadyExistsException</returns>
        public PipelineAlreadyExistsException(string message) : base(message) { }
    }
}