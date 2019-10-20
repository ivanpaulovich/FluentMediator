using System;
using System.Collections.Generic;

namespace FluentMediator.Pipelines
{
    public interface IPipelineCollection<TPipeline>
        where TPipeline : class, ITypedPipeline
    {
        void Add(TPipeline pipeline);
        TPipeline Get(Type requestType);
        bool Contains(Type requestType, out TPipeline? pipeline);
        IEnumerable<TPipeline> ToIEnumerable();
    }
}