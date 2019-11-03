namespace FluentMediator.Pipelines
{
    /// <summary>
    /// Occurs when a return function was not defined
    /// </summary>
    public sealed class ReturnFunctionIsNullException : MediatorException
    {
        /// <summary>
        /// Instantiate the Exception
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <returns>An exception</returns>
        public ReturnFunctionIsNullException(string message) : base(message) { }
    }
}