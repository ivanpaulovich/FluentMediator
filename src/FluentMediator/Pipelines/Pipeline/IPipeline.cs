using System;

namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipeline
    {
        Type RequestType { get; }
        void Publish(GetService getService, object request);
        TResult Send<TResult>(GetService getService, object request);
    }
}