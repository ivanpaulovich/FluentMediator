using System;
using System.Collections.Generic;

namespace FluentMediator
{
    public class CancellableAsyncPipelineCollection
    {
        private IDictionary<Type, ICancellableAsyncPipeline> _pipelines;

        public CancellableAsyncPipelineCollection()
        {
            _pipelines = new Dictionary<Type, ICancellableAsyncPipeline>();
        }

        public void Add<Request>(ICancellableAsyncPipeline pipeline)
        {
            _pipelines.Add(typeof(Request), pipeline);
        }

        public bool Contains<Request>(out ICancellableAsyncPipeline pipeline)
        {
            if (!_pipelines.ContainsKey(typeof(Request)))
            {
                pipeline = null!;
                return false;
            }

            pipeline = _pipelines[typeof(Request)];
            return true;
        }
    }
}