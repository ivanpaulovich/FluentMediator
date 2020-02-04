using System;

namespace FluentMediator
{
    /// <summary>
    /// On Error Event.
    /// </summary>
    public class PipelineNotFoundEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public object Message { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PipelineNotFoundEventArgs(object message)
        {
            this.Message = message;
        }
    }
}