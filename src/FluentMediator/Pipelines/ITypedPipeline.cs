using System;

namespace FluentMediator.Pipelines
{
    public interface ITypedPipeline
    {
        Type RequestType { get; }
    }
}