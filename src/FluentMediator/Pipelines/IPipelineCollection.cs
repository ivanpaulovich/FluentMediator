using System;

namespace FluentMediator.Pipelines
{
    public interface IPipelineCollection<TPipeline> where TPipeline : class
    {
        void Add<TRequest>(TPipeline pipeline);
        TPipeline Get(object request);
        bool Contains(Type messageType, out TPipeline? pipeline);
    }
}