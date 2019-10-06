using System;
using System.Collections.Generic;

namespace FluentMediator
{
    public class PipelineCollection
    {
        private IDictionary<Type, IPipeline> _pipelines;

        public PipelineCollection()
        {
            _pipelines = new Dictionary<Type, IPipeline>();
        }

        public void Add<Request>(IPipeline pipeline)
        {
            _pipelines.Add(typeof(Request), pipeline);
        }

        public bool Contains<Request>(out IPipeline pipeline)
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