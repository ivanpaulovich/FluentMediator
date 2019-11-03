using System;

namespace FluentMediator.Pipelines
{
    /// <summary>
    /// A Typed Pipeline
    /// </summary>
    public interface ITypedPipeline
    {
        /// <summary>
        /// A RequestType
        /// </summary>
        /// <value></value>
        Type RequestType { get; }
    }
}