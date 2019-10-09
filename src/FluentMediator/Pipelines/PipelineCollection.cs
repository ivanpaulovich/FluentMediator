using System;
using System.Collections.Generic;

namespace FluentMediator.Pipelines
{
    public class PipelineCollection<P> where P : class
    {
        private readonly IDictionary<Type, P> _pipelines;

        public PipelineCollection()
        {
            _pipelines = new Dictionary<Type, P>();
        }

        public void Add<Request>(P pipeline)
        {
            _pipelines.Add(typeof(Request), pipeline);
        }

        public P Get(object request)
        {
            if (_pipelines.TryGetValue(request.GetType(), out var p))
            {
                return p;
            }
            
            throw new Exception("Pipeline Not Found.");
        }

        public bool Contains(Type messageType, out P? pipeline)
        {
            if (!_pipelines.ContainsKey(messageType))
            {
                pipeline = default(P);
                return false;
            }

            pipeline = _pipelines[messageType];
            return true;
        }
    }
}