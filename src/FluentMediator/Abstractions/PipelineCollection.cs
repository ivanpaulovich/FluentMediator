using System;
using System.Collections.Generic;

namespace FluentMediator
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

        public bool Contains<Request>(out P? pipeline)
        {
            if (!_pipelines.ContainsKey(typeof(Request)))
            {
                pipeline = default(P);
                return false;
            }

            pipeline = _pipelines[typeof(Request)];
            return true;
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