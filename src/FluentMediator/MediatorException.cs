using System;

namespace FluentMediator
{
    /// <summary>
    /// All Exceptions from FluentMediator are derived from MediatorException
    /// </summary>
    public class MediatorException : Exception
    {
        /// <summary>
        /// Instantiate a MediatorException
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>The exception</returns>
        public MediatorException(string message) : base(message) { }
    }
}