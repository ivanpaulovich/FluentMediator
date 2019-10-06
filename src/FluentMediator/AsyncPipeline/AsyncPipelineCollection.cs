using System;
using System.Collections.Generic;

namespace FluentMediator
{
    public class AsyncPipelineCollection
    {
        private IDictionary<Type, IAsyncPipeline> _pipelines;

        public AsyncPipelineCollection()
        {
            _pipelines = new Dictionary<Type, IAsyncPipeline>();
        }

        public void Add<Request>(IAsyncPipeline pipeline)
        {
            _pipelines.Add(typeof(Request), pipeline);
        }

        public bool Contains<Request>(out IAsyncPipeline pipeline)
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