namespace FluentMediator
{
    /// <summary>
    /// Occurs when the message is null
    /// </summary>
    public class NullRequestException : MediatorException
    {
        /// <summary>
        /// Instantiate an Exception
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>An Exception instance</returns>
        public NullRequestException(string message) : base(message) { }
    }
}