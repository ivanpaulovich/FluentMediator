using System;
using System.Collections.Generic;

namespace FluentMediator.Pipelines
{
    public interface IPipelineCollection<TPipeline> : IEnumerable<TPipeline>
        where TPipeline : class
        {
            void Add<TRequest>(TPipeline pipeline);
            TPipeline Get(Type requestType);
            bool Contains(Type messageType, out TPipeline? pipeline);
        }
}